using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class BaseNotFoundException : Exception
    {
        public BaseNotFoundException(string entityName, int id)
            : base($"Nie znaleziono {entityName} o ID {id}"){}
    }
}
