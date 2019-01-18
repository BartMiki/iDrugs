using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class MedicineNotFoundException : BaseNotFoundException
    {
        public MedicineNotFoundException(int id)
            : base("leku", id){}
    }
}
