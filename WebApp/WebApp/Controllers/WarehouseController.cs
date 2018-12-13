using AutoMapper;
using Common.Utils;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.MedicineModels;
using WebApp.Models.WarehouseModels;
using static AutoMapper.Mapper;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace WebApp.Controllers
{
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseRepo _warehouseRepo;
        private readonly IMedicineRepo _medicineRepo;

        public WarehouseController(IWarehouseRepo warehouseRepo, IMedicineRepo medicineRepo)
        {
            _warehouseRepo = warehouseRepo;
            _medicineRepo = medicineRepo;
        }

        public IActionResult Index()
        {
            DisplayErrorFromRedirectIfNecessary();

            var result = _warehouseRepo.Get();

            if (result.IsSuccess)
            {
                var model = Mapper.Map<IEnumerable<WarehouseItemViewModel>>(result.Value);
                return View(model);
            }

            AddLocalError(result.FailureMessage);
            return View(Enumerable.Empty<WarehouseItemViewModel>());
        }

        public IActionResult Create()
        {
            var model = new AddWarehouseItemViewModel
            {
                MedicineList = GetMedicineSelectList().Value
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(AddWarehouseItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MedicineList = GetMedicineSelectList().Value;
                return View(model);
            }
            var entity = Map<WarehouseItemViewModel>(model);

            _warehouseRepo.Add(entity);

            return RedirectToIndex();
        }

        public IActionResult Edit(int id)
        {
            var result = _warehouseRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var model = Map<AddWarehouseItemViewModel>((WarehouseItemViewModel)result.Value);

            model.MedicineList = GetMedicineSelectList().Value;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AddWarehouseItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.MedicineList = GetMedicineSelectList().Value;
                return View(model);
            }

            var result = _warehouseRepo.Add(Map<WarehouseItemViewModel>(model));

            if (!result.IsSuccess)
            {
                AddLocalError(result.FailureMessage);
                return View(model);
            }

            return RedirectToIndex();
        }

        private Result<IEnumerable<MedicineSelectModel>> GetMedicineSelectList()
        {
            return Try(() =>
            {
                var result = _medicineRepo.Get();

                if (!result.IsSuccess) throw new Exception(result.FailureMessage);

                var temp = Map<IEnumerable<MedicineViewModel>>(result.Value);
                return Map<IEnumerable<MedicineSelectModel>>(temp);
            }, typeof(WarehouseController));
        }
    }
}