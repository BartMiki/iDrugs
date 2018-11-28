using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using static Common.Utils.DatabaseExceptionHandler;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repos
{
    public class MedicineEfRepo : IMedicineRepo
    {
        private readonly iDrugsEntities _context;

        public MedicineEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result Add(Medicine medicine)
        {
            var result = Try(() =>
            {
                _context.Medicines.Add(medicine);
                _context.SaveChanges();
            });

            return result;
        }

        public Result Delete(int id)
        {
            var result = Try(() =>
            {
                _context.DeleteMedicine(id);
                _context.SaveChanges();
            });

            return result;
        }

        public Result Edit(Medicine medicine)
        {
            var result = Try(() =>
            {
                var entity = _context.Medicines.Find(medicine.Id);

                if (entity == null) throw new MedicineNotFoundException(medicine.Id);

                _context.Entry(medicine)
                    .CurrentValues.SetValues(medicine);

                _context.SaveChanges();
            });

            return result;
        }

        public Result<IEnumerable<Medicine>> Get()
        {
            var result = Try(() =>
            {
                return _context.Medicines.AsEnumerable();
            });

            return result;
        }

        public Result<Medicine> Get(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Medicines.Find(id);

                return entity ?? throw new MedicineNotFoundException(id);
            });

            return result;
        }

        public Result MakeExpired(int id)
        {
            throw new NotImplementedException();
        }
    }
}
