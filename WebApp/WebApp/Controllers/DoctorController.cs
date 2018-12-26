using AutoMapper;
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

        public DoctorController(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public IActionResult Index()
        {
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
            var result = _doctorRepo.Remove(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = Mapper.Map<Doctor>(model);
            var result = _doctorRepo.Add(entity);

            if (result.IsSuccess) return RedirectToIndex();

            AddLocalError(result.FailureMessage);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var result = _doctorRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var model = Mapper.Map<DoctorViewModel>(result.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var entity = Mapper.Map<Doctor>(model);
            var result = _doctorRepo.Edit(entity);

            if (result.IsSuccess) return RedirectToIndex();

            AddLocalError(result.FailureMessage);
            return View(model);
        }

        public IActionResult RemoveLicesnse(int id)
        {
            var result = _doctorRepo.RemoveLicence(id);

            if (result.IsSuccess) return RedirectToIndex();

            return RedirectToIndex(result.FailureMessage);
        }

        public IActionResult Details(int id)
        {
            var result = _doctorRepo.Get(id);

            if (!result.IsSuccess) return RedirectToIndex(result.FailureMessage);

            var model = Mapper.Map<DoctorViewModel>(result.Value);

            return View(model);
        }
    }
}