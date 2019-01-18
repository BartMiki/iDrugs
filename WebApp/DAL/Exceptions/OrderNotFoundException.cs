using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class OrderNotFoundException : BaseNotFoundException
    {
        public OrderNotFoundException(int orderId)
            : base("zamówienia", orderId){}
    }
}
