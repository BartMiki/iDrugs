using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWarehouseRepo
    {
        Result<IEnumerable<MedicineWarehouse>> Get();
        Result<MedicineWarehouse> Get(int id);
        Result Add(MedicineWarehouse entity);
    }
}
