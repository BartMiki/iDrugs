using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepo
    {
        Result<IEnumerable<Order>> Get();
        Result<Order> Get(int id);
        Result<int> Create(int aptohecaryId);
        Result AddOrderItem(int orderId, OrderItem orderItem);
        Result SendOrder(int id);
        Result RemoveOrder(int id);
        Result EditOrderItem(OrderItem id);
        Result DeleteOrderItem(int orderId, int itemId);
    }
}
