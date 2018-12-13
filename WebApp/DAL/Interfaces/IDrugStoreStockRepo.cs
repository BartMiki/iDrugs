using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDrugStoreStockRepo
    {
        Result<IEnumerable<DrugStoreAvailableMedicine>> Get();
    }
}
