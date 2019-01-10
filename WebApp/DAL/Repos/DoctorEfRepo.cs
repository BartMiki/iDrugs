using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace DAL.Repos
{
    public class DoctorEfRepo : IDoctorRepo
    {
        private readonly iDrugsEntities _context;

        public DoctorEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result Add(Doctor doctor)
        {
            var result = Try(() => 
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();

            }, typeof(DoctorEfRepo));

            return result;
        }

        public Result Edit(Doctor doctor)
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.Find(doctor.Id);

                if (entity == null) throw new DoctorNotFoundException(doctor.Id);

                if (entity.RowVersion != doctor.RowVersion)
                    throw new DBConcurrencyException("Informacje o aptekarzu zostały zmienione, przez innego użytkownika");

                doctor.RowVersion++;

                _context.Entry(entity)
                    .CurrentValues.SetValues(doctor);

                _context.SaveChanges();
            }, typeof(DoctorEfRepo));

            return result;
        }

        public Result<IEnumerable<Doctor>> Get()
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.ToList().AsEnumerable();

                return entity;
            }, typeof(DoctorEfRepo));

            return result;
        }

        public Result<Doctor> Get(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.Find(id);

                if (entity == null) throw new DoctorNotFoundException(id);

                return entity;
            }, typeof(DoctorEfRepo));
            return result;
        }

        public Result Remove(int id)
        {
            var result = Try(() =>
            {
                var doctor = new Doctor { Id = id };
                _context.Doctors.Attach(doctor);
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }, typeof(DoctorEfRepo));

            return result;
        }

        public Result RemoveLicence(int id)
        {
            var result = Try(() =>
            {
                _context.RemoveDoctorLicense(id);
                _context.SaveChanges();
            }, typeof(DoctorEfRepo));

            return result;
        }
    }
}
