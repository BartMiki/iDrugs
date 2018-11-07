using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IApothecaryRepo : IBaseRepo<iDrugsEntities>
    {
        Result<IEnumerable<Apothecary>> Get();
        Result<Apothecary> Get(int id);
        Result Add(Apothecary apothecary);
        Result Fire(int id);
        Result Remove(int id);
        Result<Apothecary> Update(Apothecary apothecary);
    }
}
