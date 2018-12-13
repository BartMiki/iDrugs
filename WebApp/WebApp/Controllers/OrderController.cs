using Common.Utils;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.MedicineModels;
using WebApp.Models.OrderModels;
using static AutoMapper.Mapper;
using static Common.Handlers.StaticDatabaseExceptionHandler;
using static WebApp.Models.ApothecaryModels.ApothecarySelectViewModel;
using static WebApp.Models.ApothecaryModels.ApothecaryViewModel;

namespace WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMedicineRepo _medicineRepo;

        public OrderController(IApothecaryRepo apothecaryRepo, IOrderRepo orderRepo, IMedicineRepo medicineRepo)
        {
            _apothecaryRepo = apothecaryRepo;
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
        
            var result = _orderRepo.Create(model.SelectedId);

            if (result.IsSuccess) return RedirectToDetails(result.Value);

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult SendOrder(int orderId)
        {
            var result = _orderRepo.SendOrder(orderId);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            return RedirectToIndex();
        }

        public IActionResult AddOrderItem(int orderId)
        {
            var result = GetMedicineSelectList();

            if (!result.IsSuccess) return RedirectToDetails(orderId, result.FailureMessage);

            var model = new AddOrderItemViewModel
            {
                OrderId = orderId,
                MedicineList = result.Value
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddOrderItem(AddOrderItemViewModel model)
        {
            var medicineListResult = GetMedicineSelectList();

            if (!medicineListResult.IsSuccess)
                return RedirectToDetails(model.OrderId, medicineListResult.FailureMessage);

            if (!ModelState.IsValid)
            {
                model.MedicineList = medicineListResult.Value;
                return View(model);
            }

            var result = _orderRepo.AddOrderItem(model.OrderId, model);

            if (!result.IsSuccess) AddErrorForRedirect(result.FailureMessage);

            return RedirectToDetails(model.OrderId);
        }

        public IActionResult Delete(int id)
        {
            var result = _orderRepo.RemoveOrder(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        [HttpGet]
        public IActionResult EditOrderItem(int orderId, int itemId)
        {
            var result = _orderRepo.Get(orderId);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var item = result.Value.OrderItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null) return RedirectToDetails(orderId, $"Nie znaleziono pozycji na zamówieniu o Id {itemId}");

            return View((EditOrderItemViewModel)item);
        }

        [HttpPost]
        public IActionResult EditOrderItem(EditOrderItemViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _orderRepo.EditOrderItem(model);

            if (!result.IsSuccess) return RedirectToDetails(model.OrderId, result.FailureMessage);

            return RedirectToDetails(model.OrderId);
        }

        public IActionResult DeleteOrderItem(int orderId, int itemId)
        {
            var result = _orderRepo.DeleteOrderItem(orderId, itemId);

            if (!result.IsSuccess) return RedirectToDetails(orderId, result.FailureMessage);

            return RedirectToDetails(orderId);
        }

        private IActionResult RedirectToDetails(int orderId) => RedirectToDetails(orderId, null);

        private IActionResult RedirectToDetails(int orderId, string errorMsg)
        {
            if (!string.IsNullOrEmpty(errorMsg)) AddErrorForRedirect(errorMsg);
            return RedirectToAction(nameof(Details), new { id = orderId });
        }

        private Result<IEnumerable<MedicineSelectModel>> GetMedicineSelectList()
        {
            return Try(() => {
                var result = _medicineRepo.Get();

                if (!result.IsSuccess) throw new Exception(result.FailureMessage);

                var temp = Map<IEnumerable<MedicineViewModel>>(result.Value);
                return Map<IEnumerable<MedicineSelectModel>>(temp);
            }, typeof(OrderController));
        }
    }
}