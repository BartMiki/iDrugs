using Common.Utils;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace DAL.Repos
{
    public class DrugStoreStockEfRepo : IDrugStoreStockRepo
    {
        private readonly iDrugsEntities _context;

        public DrugStoreStockEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result<IEnumerable<DrugStoreAvailableMedicine>> Get()
        {
            var result = Try(() => 
            {
                return _context.DrugStoreAvailableMedicines.ToArray().AsEnumerable();
            }, typeof(DrugStoreStockEfRepo));

            return result;
        }
    }
}
