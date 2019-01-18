using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class OrderItemNotFoundException : BaseNotFoundException
    {
        public OrderItemNotFoundException(int id) : base("pozycji na zamówieniu", id)
        {
        }
    }
}
