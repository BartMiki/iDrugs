using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class WarehouseItemNotFoundException : BaseNotFoundException
    {
        public WarehouseItemNotFoundException(int id) : base("leku w magazynie", id) {}
    }
}
