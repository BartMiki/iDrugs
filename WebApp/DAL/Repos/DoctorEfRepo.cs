using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Utils.DatabaseExceptionHandler;
using static DAL.Utils.TransactionExtension;

namespace DAL.Repos
{
    public class DoctorEfRepo : IDoctorRepo
    {
        private readonly iDrugsEntities _context;

        public DoctorEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result Edit(Doctor doctor)
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.Find(doctor.Id);

                if (entity == null) throw new DoctorNotFoundException(doctor.Id);

                _context.Entry(doctor)
                    .CurrentValues.SetValues(doctor);

                _context.SaveChanges();
            });

            return result;
        }

        public Result<IEnumerable<Doctor>> Get()
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.ToList().AsEnumerable();

                return entity;
            });

            return result;
        }

        public Result<Doctor> Get(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Doctors.Find(id);

                if (entity == null) throw new DoctorNotFoundException(id);

                return entity;
            });
            return result;
        }

        public Result Remove(Doctor doctor)
        {
            var result = Try(() =>
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            });

            return result;
        }

        public Result RemoveLicence(Doctor doctor)
        {
            var result = Try(() =>
            {
                _context.Entry(doctor).CurrentValues.SetValues(doctor);
                _context.SaveChanges();
            });

            return result;
        }
    }
}
