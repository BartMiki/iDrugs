using AutoMapper;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.OrderModels;
using static Common.Utils.DatabaseExceptionHandler;
using static WebApp.Models.ApothecaryModels.ApothecarySelectViewModel;
using static WebApp.Models.ApothecaryModels.ApothecaryViewModel;
using static AutoMapper.Mapper;
using WebApp.Models.MedicineModels;

namespace WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly iDrugsEntities _context;
        private readonly IOrderRepo _orderRepo;
        private readonly IMedicineRepo _medicineRepo;

        public OrderController(IApothecaryRepo apothecaryRepo, iDrugsEntities context, IOrderRepo orderRepo, IMedicineRepo medicineRepo)
        {
            _apothecaryRepo = apothecaryRepo;
            _context = context;
            _orderRepo = orderRepo;
            _medicineRepo = medicineRepo;
        }

        public IActionResult Index()
        {
            DisplayErrorFromRedirectIfNecessary();

            var result = _orderRepo.Get();

            if (result.IsSuccess)
            {
                var model = Map<IEnumerable<OrderViewModel>>(result.Value);
                return View(model);
            }
            else
            {
                AddLocalError(result.FailureMessage);
                return View(Enumerable.Empty<OrderViewModel>());
            }
        }

        public IActionResult Details(int id)
        {
            DisplayErrorFromRedirectIfNecessary();

            var result = _orderRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var order = Map<OrderDetailViewModel>(result.Value);
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

            var result = Try(() =>
            {
                var res = _context.CreateOrder(model.SelectedId).FirstOrDefault();

                if (!res.HasValue) throw new Exception("Nie znaleziono nowo dodanego zamówienia.");

                return res.Value;
            });

            if (result.IsSuccess) return RedirectToDetails(result.Value);

            return RedirectToIndex(result.FailureMessage);
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
            var result = _medicineRepo.Get();
            
            if (!result.IsSuccess)
            {
                AddErrorForRedirect(result.FailureMessage);
                return RedirectToDetails(orderId);
            }

            var temp = Map<IEnumerable<MedicineViewModel>>(result.Value);
            var medicineList = Map<IEnumerable<MedicineSelectModel>>(temp);

            var model = new AddOrderItemViewModel
            {
                OrderId = orderId,
                MedicineList = medicineList
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrderItem(AddOrderItemViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _orderRepo.AddOrderItem(model.OrderId, model);

            if (!result.IsSuccess) AddErrorForRedirect(result.FailureMessage);

            return RedirectToDetails(model.OrderId);
        }

        public IActionResult Delete(int id)
        {
            var result = _orderRepo.RemoveOrder(id);

            if(result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        private IActionResult RedirectToDetails(int orderId) => RedirectToAction(nameof(Details), new { id = orderId });
    }
}