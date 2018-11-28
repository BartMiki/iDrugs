using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Common.Utils.DatabaseExceptionHandler;

namespace DAL.Repos
{
    public class OrderEfRepo : IOrderRepo
    {
        private readonly iDrugsEntities _context;

        public OrderEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result Add(Order order)
        {
            var result = Try(() =>
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            });

            return result;
        }

        public Result AddOrderItem(int orderId, OrderItem orderItem)
        {
            var result = Try(() =>
            {
                var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

                if (order == null) throw new OrderNotFoundException(orderId);

                order.OrderItems.Add(orderItem);
                _context.SaveChanges();

            });

            return result;
        }

        public Result<IEnumerable<Order>> Get()
        {
            var result = Try(() => _context.Orders.ToArray().AsEnumerable());

            return result;
        }

        public Result<Order> Get(int id)
        {
            var result = Try(() =>
            {
                var order = _context.Orders
                    .Where(o => o.Id == id)
                    .Include(o => o.OrderItems)
                    .Include(o => o.Apothecary)
                    .FirstOrDefault();

                if (order == null) throw new OrderNotFoundException(id);

                return order;
            });

            return result;
        }

        public Result RemoveOrder(int id)
        {
            var result = _context.BeginTransaction(() =>
            {
                var order = _context.Orders.Find(id);

                if (order == null) throw new OrderNotFoundException(id);

                foreach (var item in order.OrderItems.ToArray())
                {
                    _context.OrderItems.Remove(item);
                }

                _context.Orders.Remove(order);
                _context.SaveChanges();

                //throw new Exception("Test");
            });

            return result;
        }

        public Result SendOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
