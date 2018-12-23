using Common.Utils;
using DAL.Exceptions;
using DAL.Interfaces;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace DAL.Repos
{
    public class OrderEfRepo : IOrderRepo
    {
        private readonly iDrugsEntities _context;

        public OrderEfRepo(iDrugsEntities context)
        {
            _context = context;
        }

        public Result<int> Create(int apothecaryId)
        {
            var result = Try(() =>
            {
                var res = _context.CreateOrder(apothecaryId).FirstOrDefault();

                if (!res.HasValue) throw new Exception("Nie znaleziono nowo dodanego zamówienia.");

                _context.SaveChanges();

                return res.Value;
            }, typeof(OrderEfRepo));

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

            }, typeof(OrderEfRepo));

            return result;
        }

        public Result DeleteOrderItem(int orderId, int itemId)
        {
            var result = Try(() =>
            {
                var entity = _context.OrderItems.Find(itemId);

                if (entity == null) throw new OrderItemNotFoundException(itemId);

                _context.OrderItems.Remove(entity);
                _context.SaveChanges();
            }, typeof(OrderEfRepo));

            return result;
        }

        public Result EditOrderItem(OrderItem item)
        {
            var result = Try(() =>
            {
                var entity = _context.OrderItems.Find(item.Id);

                if (entity == null) throw new OrderItemNotFoundException(item.Id);

                entity.Quantity = item.Quantity;
                _context.SaveChanges();
            }, typeof(OrderEfRepo));

            return result;
        }

        public Result<IEnumerable<Order>> Get()
        {
            var result = Try(() => _context.Orders.ToArray().AsEnumerable(), typeof(OrderEfRepo));

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
            }, typeof(OrderEfRepo));

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
            }, typeof(OrderEfRepo));

            return result;
        }

        public Result SendOrder(int id)
        {
            var result = _context.BeginTransaction(() =>
            {
                var order = _context.Orders.Find(id);

                if (order == null) throw new OrderNotFoundException(id);

                if (order.SendOrderDate.HasValue)
                    throw new Exception("Zamówienie zostało już wysłane, nie można wysłać go ponownie.");

                if (order.OrderItems.Count == 0)
                    throw new Exception("Zamównienie nie może być puste");

                foreach (var orderItem in order.OrderItems)
                {
                    // Getting items from Warehouse
                    var warehouseItem = _context.MedicineWarehouses.FirstOrDefault(x => x.MedicineId == orderItem.MedicineId);

                    if (warehouseItem == null || warehouseItem.Quantity == 0)
                        throw new Exception($"W hurtowni nie ma leku o nazwie {orderItem.Medicine.Name}");

                    var qunatityAfterOrder = warehouseItem.Quantity - orderItem.Quantity;
                    if (qunatityAfterOrder < 0)
                        throw new Exception($"Nie można zrealizować zamówienia - na magazynie brakuje {qunatityAfterOrder}"
                            + $" sztuk leku {orderItem.Medicine.Name}");

                    warehouseItem.Quantity -= orderItem.Quantity;

                    // Inserting items into Drug Store Available Medicines
                    var drugStoreItem = _context.DrugStoreAvailableMedicines.FirstOrDefault(x => x.MedicineId == orderItem.MedicineId);

                    if (drugStoreItem == null)
                    {
                        drugStoreItem = new DrugStoreAvailableMedicine
                        {
                            MedicineId = orderItem.MedicineId,
                            Quantity = orderItem.Quantity
                        };

                        _context.DrugStoreAvailableMedicines.Add(drugStoreItem);
                    }
                    else
                    {
                        drugStoreItem.Quantity += orderItem.Quantity;
                    }
                }

                order.SendOrderDate = DateTime.Now;

                _context.SaveChanges();
            }, typeof(OrderEfRepo));

            return result;
        }
    }
}
