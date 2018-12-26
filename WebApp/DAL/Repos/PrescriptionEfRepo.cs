using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using DAL.Utils;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace DAL.Repos
{
    public class PrescriptionEfRepo : IPrescriptionRepo
    {
        private readonly iDrugsEntities _context;

        public PrescriptionEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result AcceptCreated(int id)
        {
            return Try(() =>
            {
                _context.AcceptCreatedPrescription(id);
            }, typeof(PrescriptionEfRepo));
        }

        public Result AddPrescription(Prescription entity)
        {
            return Try(() =>
            {
                _context.Prescriptions.Add(entity);
                _context.SaveChanges();
            }, typeof(PrescriptionEfRepo));
        }

        public Result AddPrescriptionItem(int prescriptionId, PrescriptionItem item)
        {
            return Try(() =>
            {
                var entity = _context.Prescriptions.Find(prescriptionId);

                if (entity == null) throw new PrescriptionNotFoundException(prescriptionId);

                entity.PrescriptionItems.Add(item);
                _context.SaveChanges();

            }, typeof(PrescriptionEfRepo));
        }

        public Result DeletePrescription(int id)
        {
            return _context.BeginTransaction(() => 
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @id", new { id });
                _context.Database.ExecuteSqlCommand("DELETE FROM Prescription WHERE Id = @id", new { id });

            }, typeof(PrescriptionEfRepo));
        }

        public Result DeletePrescriptionItem(int prescriptionId, int itemId)
        {
            return Try(() => 
            {
                _context.Database.ExecuteSqlCommand("DELETE FROM PrescriptionItem WHERE PrescriptionId = @pId AND Id = @id", 
                    new { id = itemId, pId = prescriptionId});

            }, typeof(PrescriptionEfRepo));
        }

        public Result EditPrescription(Prescription prescription)
        {
            return Try(() => 
            {
                var entity = _context.Prescriptions.Find(prescription.Id);

                if (entity == null) throw new PrescriptionNotFoundException(prescription.Id);

                _context.Entry(entity)
                    .CurrentValues.SetValues(prescription);

            }, typeof(PrescriptionEfRepo));
        }

        public Result EditPrescriptionItem(PrescriptionItem item)
        {
            return Try(() => 
            {
                var entity = _context.PrescriptionItems.Find(item.Id);

                if (entity == null) throw new PrescriptionItemNotFoundException(item.PrescriptionId, item.Id);

                _context.Entry(entity)
                    .CurrentValues.SetValues(item);

            }, typeof(PrescriptionEfRepo));
        }

        public Result<IEnumerable<Prescription>> Get()
        {
            return Try(() => 
            {
                return _context.Prescriptions.AsEnumerable();
            }, typeof(PrescriptionEfRepo));
        }

        public Result<Prescription> Get(int id)
        {
            return Try(() => 
            {
                var entity = _context.Prescriptions
                    .Include(x => x.Apothecary)
                    .Include(x => x.Doctor)
                    .Include(x => x.PrescriptionItems)
                    .FirstOrDefault(x => x.Id == id);

                if (entity == null) throw new PrescriptionNotFoundException(id);

                return entity;
            }, typeof(PrescriptionEfRepo));
        }

        public Result<PrescriptionItem> GetItem(int id)
        {
            return Try(() => 
            {
                var entity = _context.PrescriptionItems.Find(id);

                if (entity == null) throw new PrescriptionItemNotFoundException(id);

                return entity;

            }, typeof(PrescriptionEfRepo));
        }
    }
}
