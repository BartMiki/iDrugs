using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class DoctorNotFoundException : BaseNotFoundException
    {
        public DoctorNotFoundException(int id) 
            : base("lekarza", id){}
    }
}
