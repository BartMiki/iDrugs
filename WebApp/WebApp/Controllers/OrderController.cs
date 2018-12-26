using Common.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApp.Interfaces;
using WebApp.Models.OrderModels;
using static AutoMapper.Mapper;

namespace WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IMedicineRepo _medicineRepo;
        private readonly ILogger<OrderController> _logger;
        private readonly ISelectService _selectService;

        public OrderController(IApothecaryRepo apothecaryRepo,
            IOrderRepo orderRepo,
            IMedicineRepo medicineRepo,
            ILogger<OrderController> logger,
            ISelectService selectService)
        {
            _apothecaryRepo = apothecaryRepo;
            _orderRepo = orderRepo;
            _medicineRepo = medicineRepo;
            _logger = logger;
            _selectService = selectService;
        }

        public IActionResult Index()
        {
            _logger.LogInfo($"Zapytanie do metody Index()");

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
            _logger.LogInfo($"Zapytanie do metody Details(id)", new { id });

            DisplayErrorFromRedirectIfNecessary();

            var result = _orderRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var order = Map<OrderDetailViewModel>(result.Value);
            return View(order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInfo($"Zapytanie do metody Create()");

            var result = _selectService.GetApothecarySelectList();

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage); 

            var model = new CreateOrderViewModel
            {
                ApothecaryList = result.Value
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            _logger.LogInfo($"Zapytanie do metody Create(model)", new { model });

            if (!ModelState.IsValid) return View(model);

            var result = _orderRepo.Create(model.SelectedId);

            if (result.IsSuccess) return RedirectToDetails(result.Value);

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult SendOrder(int orderId)
        {
            _logger.LogInfo($"Zapytanie do metody SendOrder(orderId)", new { orderId });

            var result = _orderRepo.SendOrder(orderId);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            return RedirectToIndex();
        }

        public IActionResult AddOrderItem(int orderId)
        {
            _logger.LogInfo($"Zapytanie do metody AddOrderItem(orderId)", new { orderId });

            var result = _selectService.GetMedicineSelectList();

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
            _logger.LogInfo($"Zapytanie do metody AddOrderItem(model)", new { model });

            var medicineListResult = _selectService.GetMedicineSelectList();

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
            _logger.LogInfo($"Zapytanie do metody Delete(id)", new { id });

            var result = _orderRepo.RemoveOrder(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        [HttpGet]
        public IActionResult EditOrderItem(int orderId, int itemId)
        {
            _logger.LogInfo($"Zapytanie do metody EditOrderItem(orderId, itemId)", new { orderId, itemId });

            var result = _orderRepo.Get(orderId);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var item = result.Value.OrderItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null) return RedirectToDetails(orderId, $"Nie znaleziono pozycji na zamówieniu o Id {itemId}");

            return View((EditOrderItemViewModel)item);
        }

        [HttpPost]
        public IActionResult EditOrderItem(EditOrderItemViewModel model)
        {
            _logger.LogInfo($"Zapytanie do metody EditOrderItem(model)", new { model });

            if (!ModelState.IsValid) return View(model);

            var result = _orderRepo.EditOrderItem(model);

            if (!result.IsSuccess) return RedirectToDetails(model.OrderId, result.FailureMessage);

            return RedirectToDetails(model.OrderId);
        }

        public IActionResult DeleteOrderItem(int orderId, int itemId)
        {
            _logger.LogInfo($"Zapytanie do metody DeleteOrderItem(orderId, itemId)", new { orderId, itemId });

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
    }
}