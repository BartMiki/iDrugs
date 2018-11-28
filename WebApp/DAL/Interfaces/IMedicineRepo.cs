using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMedicineRepo
    {
        Result<IEnumerable<Medicine>> Get();
        Result<Medicine> Get(int id);
        Result MakeExpired(int id);
        Result Add(Medicine medicine);
        Result Edit(Medicine medicine);
        Result Delete(int id); 
    }
}
