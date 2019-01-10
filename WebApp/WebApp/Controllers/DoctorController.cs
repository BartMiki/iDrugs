using AutoMapper;
using Common.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models.DoctorModels;

namespace WebApp.Controllers
{
    public class DoctorController : BaseController
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorRepo doctorRepo, ILogger<DoctorController> logger)
        {
            _doctorRepo = doctorRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInfo($"Zapytanie do metody Index()");

            DisplayErrorFromRedirectIfNecessary();

            var result = _doctorRepo.Get();

            if (!result.IsSuccess)
            {
                AddLocalError(result.FailureMessage);
                return View(Enumerable.Empty<DoctorViewModel>());
            }

            var model = Mapper.Map<IEnumerable<DoctorViewModel>>(result.Value);

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _logger.LogInfo($"Zapytanie do metody Delete()", new { id });

            var result = _doctorRepo.Remove(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult Create()
        {
            _logger.LogInfo($"Zapytanie do metody Create()");

            var model = new DoctorViewModel
            {
                HasValidMedicalLicense = true,
                RowVersion = 1
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel model)
        {
            _logger.LogInfo($"Zapytanie do metody Create()", new { model });

            if (!ModelState.IsValid) return View(model);

            var entity = Mapper.Map<Doctor>(model);
            var result = _doctorRepo.Add(entity);

            if (result.IsSuccess) return RedirectToIndex();

            AddLocalError(result.FailureMessage);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            _logger.LogInfo($"Zapytanie do metody Edit()", new { id });

            DisplayErrorFromRedirectIfNecessary();

            var result = _doctorRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var model = Mapper.Map<DoctorViewModel>(result.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel model)
        {
            _logger.LogInfo($"Zapytanie do metody Edit()", new { model });

            if (!ModelState.IsValid) return View(model);

            var entity = Mapper.Map<Doctor>(model);
            var result = _doctorRepo.Edit(entity);

            if (result.IsSuccess) return RedirectToAction("Details", new { id = model.Id }); ;

            AddErrorForRedirect(result.FailureMessage);
            return RedirectToAction(nameof(Edit), new { id = model.Id});
        }

        public IActionResult RemoveLicesnse(int id)
        {
            _logger.LogInfo($"Zapytanie do metody RemoveLicesnse()", new { id });

            var result = _doctorRepo.RemoveLicence(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult Details(int id)
        {
            _logger.LogInfo($"Zapytanie do metody Details()", new { id });

            var result = _doctorRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var model = Mapper.Map<DoctorViewModel>(result.Value);

            return View(model);
        }
    }
}