using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Common.Utils.DatabaseExceptionHandler;

namespace DAL.Repos
{
    public class WarehouseEfRepo : IWarehouseRepo
    {
        private readonly IMedicineRepo _medicineRepo;
        private readonly iDrugsEntities _context;

        public WarehouseEfRepo(IMedicineRepo medicineRepo, iDrugsEntities context)
        {
            _medicineRepo = medicineRepo;
            _context = context;
        }

        public Result Add(MedicineWarehouse entity)
        {
            var result = Try(() =>
            {
                var item = _context.MedicineWarehouses.FirstOrDefault(x => x.MedicineId == entity.MedicineId);

                if(item == null) _context.MedicineWarehouses.Add(entity);
                else item.Quantity = entity.Quantity;

                _context.SaveChanges();
            });

            return result;
        }

        public Result<IEnumerable<MedicineWarehouse>> Get()
        {
            var result = Try(() =>
            {
                var entities = _context.MedicineWarehouses.ToArray().AsEnumerable();
                return entities;
            });
            return result;
        }

        public Result<MedicineWarehouse> Get(int id)
        {
            var result = Try(() =>
            {
                var entity = _context.MedicineWarehouses
                    .Include(x => x.Medicine)
                    .FirstOrDefault(x => x.Id == id);

                if (entity == null) throw new WarehouseItemNotFoundException(id);

                return entity;
            });

            return result;
        }
    }
}
