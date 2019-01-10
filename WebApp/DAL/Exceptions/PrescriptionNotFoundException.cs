using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class PrescriptionNotFoundException : BaseNotFoundException
    {
        public PrescriptionNotFoundException(int id) : base("recepty", id){}
    }
}
