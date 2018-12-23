using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

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
            }, typeof(MedicineEfRepo));

            return result;
        }

        public Result Delete(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Medicines.Find(id);

                if (entity == null) throw new MedicineNotFoundException(id);

                _context.Medicines.Remove(entity);
                _context.SaveChanges();
            }, typeof(MedicineEfRepo));

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
            }, typeof(MedicineEfRepo));

            return result;
        }

        public Result<IEnumerable<Medicine>> Get()
        {
            var result = Try(() =>
            {
                return _context.Medicines.AsEnumerable();
            }, typeof(MedicineEfRepo));

            return result;
        }

        public Result<Medicine> Get(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Medicines.Find(id);

                return entity ?? throw new MedicineNotFoundException(id);
            }, typeof(MedicineEfRepo));

            return result;
        }

        public Result MakeExpired(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.Medicines.Find(id);

                if (entity == null) throw new MedicineNotFoundException(id);

                if (entity.Expired) throw new Exception($"Lek o Id {id} został już wycofany, nie można wycofać go ponownie");

                entity.Expired = true;
                _context.SaveChanges();

            }, typeof(MedicineEfRepo));

            return result;
        }
    }
}
