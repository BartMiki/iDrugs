using AutoMapper;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.OrderModels;
using static Common.Utils.DatabaseExceptionHandler;
using static WebApp.Models.ApothecaryModels.ApothecarySelectViewModel;
using static WebApp.Models.ApothecaryModels.ApothecaryViewModel;

namespace WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly iDrugsEntities _context;
        private readonly IOrderRepo _orderRepo;

        public OrderController(IApothecaryRepo apothecaryRepo, iDrugsEntities context, IOrderRepo orderRepo)
        {
            _apothecaryRepo = apothecaryRepo;
            _context = context;
            _orderRepo = orderRepo;
        }

        public IActionResult Index()
        {
            DisplayTempErrorIfNedded();

            var result = _orderRepo.Get();

            if (result.IsSuccess)
            {
                var model = Mapper.Map<IEnumerable<OrderViewModel>>(result.Value);
                return View(model);
            }
            else
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(Enumerable.Empty<OrderViewModel>());
            }
        }

        public IActionResult Details(int id)
        {
            var result = _orderRepo.Get(id);
            var order = Mapper.Map<OrderDetailViewModel>(result);
            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var list = ToApothecaryViewModels(_apothecaryRepo.Get().Value);

            var model = new CreateOrderViewModel
            {
                ApothecaryList = ToApothecarySelectViewModels(list)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = Try<int>(() =>
            {
                return _context.CreateOrder(model.SelectedId);
            });

            if (result.IsSuccess)
                return RedirectToAction(nameof(Details), new { id = result.Value});

            ViewBag.ErrorMsg = result.FailureMessage;
            return View(model);

        }

        public IActionResult SendOrder(int orderId)
        {
            try
            {
                using (var db = new iDrugsEntities())
                {
                    var order = db.Orders.FirstOrDefault(o => o.Id == orderId);

                    if (order.SendOrderDate.HasValue)
                        throw new Exception("Zamówienie zostało już złożone i przetworzone, nie można go ponowinie złożyć.");

                    if (order.OrderItems.Count == 0)
                        throw new Exception("Zamównienie nie może być puste.");

                    foreach (var orderItem in order.OrderItems)
                    {
                        var warehouseItem = db.MedicineWarehouses.FirstOrDefault(o => o.MedicineId == orderItem.MedicineId);
                        warehouseItem.Quantity -= orderItem.Quantity;

                        if (warehouseItem.Quantity < 0)
                            throw new Exception($"Zamówienie odrzucone, na magazynie brakuje {-warehouseItem.Quantity} sztuk leku {warehouseItem.Medicine.Name}");

                        //order.OrderDate = DateTime.Now;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction(nameof(Details), new { id = orderId });
            }
            catch (Exception ex)
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

            using (var db = new iDrugsEntities())
            {
                db.OrderItems.Add(item);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Details), new { id = item.OrderId });
        }
    }
}