using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepo<T> where T : DbContext
    {
        T Context { get; }
    }
}
