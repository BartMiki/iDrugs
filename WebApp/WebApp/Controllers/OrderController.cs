﻿using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            if(TempData.ContainsKey("ErrorMsg"))
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
                TempData.Remove("ErrorMsg");
            }

            using (var db = new iDrugsEntities())
            {
                var orders = db.Orders.ToList();
                return View(orders);
            }
        }

        public IActionResult Details(int id)
        {
            Order order = null;
            using (var db = new iDrugsEntities())
            {
                var orderDb = db.Orders.Where(o => o.Id == id).FirstOrDefault();
                order = orderDb;
                order.OrderItems = orderDb.OrderItems.ToList();
                order.Apothecary = orderDb.Apothecary;
            }
            return View(order);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Order { OrderDate = null });
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid) return View(order);

            using (var db = new iDrugsEntities())
            {
                var apothecary = (from a in db.Apothecaries
                                 where a.Id == order.ApothecaryId && a.IsEmployed == true
                                 select a).FirstOrDefault();

                if(apothecary == null)
                {
                    ViewBag.ErrorMsg = $"Aptekarz o ID {order.ApothecaryId} nie może składać zamówień, bo już tutaj nie pracuje";
                    return View(order);
                }

                db.Orders.Add(order);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SendOrder(int orderId)
        {
            try
            {
                using (var db = new iDrugsEntities())
                {
                    var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                    if (order.OrderDate.HasValue) throw new Exception("Zamówienie zostało już złożone i przetworzone, nie można go ponowinie złożyć.");

                    foreach (var orderItem in order.OrderItems)
                    {
                        var warehouseItem = db.MedicineWarehouses.FirstOrDefault(o => o.MedicineId == orderItem.MedicineId);
                        warehouseItem.Quantity -= orderItem.Quantity;
                        order.OrderDate = DateTime.Now;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction(nameof(Details), new { id = orderId });
            }
            catch(Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message + "\n" + ex.InnerException?.Message
                    + "\n" + ex.InnerException?.InnerException?.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AddOrderItem(int orderId)
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId
            };
            return View(orderItem);
        }

        [HttpPost]
        public IActionResult AddOrderItem(OrderItem item)
        {
            if (!ModelState.IsValid) return View(item);

            using(var db = new iDrugsEntities())
            {
                db.OrderItems.Add(item);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Details), new { id = item.OrderId });
        }
    }
}