using Common.Utils;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public Result<IEnumerable<MedicineWarehouse>> Get()
        {
            throw new NotImplementedException();
        }

        public Result<MedicineWarehouse> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
