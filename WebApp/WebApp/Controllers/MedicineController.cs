using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.MedicineModels;

namespace WebApp.Controllers
{
    public class MedicineController : BaseController
    {
        private readonly IMedicineRepo _medicineRepo;

        public MedicineController(IMedicineRepo medicineRepo)
        {
            _medicineRepo = medicineRepo;
        }

        public IActionResult Index()
        {
            DisplayErrorFromRedirectIfNecessary();

            var result = _medicineRepo.Get();

            if (!result.IsSuccess)
            {
                AddLocalError(result.FailureMessage);
                return View(Enumerable.Empty<MedicineViewModel>());
            }

            var model = Mapper.Map<IEnumerable<MedicineViewModel>>(result.Value);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateMedicineViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateMedicineViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _medicineRepo.Add(model);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult Details(int id)
        {
            var result = _medicineRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            return View((MedicineDetailsViewModel)result.Value);
        }

        public IActionResult Delete(int id)
        {
            var result = _medicineRepo.Delete(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            return RedirectToIndex();
        }

        public IActionResult MakeExpired(int id)
        {
            var result = _medicineRepo.MakeExpired(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            return RedirectToIndex();
        }
    }
}