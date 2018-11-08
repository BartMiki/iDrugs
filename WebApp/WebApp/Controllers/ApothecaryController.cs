using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp
{
    public class ApothecaryController : BaseController
    {
        private readonly IApothecaryRepo _repo;

        public ApothecaryController(IApothecaryRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            ViewBag.ErrorMsg = string.Empty;
            if (TempData.ContainsKey("ErrorMsg"))
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
                TempData.Remove("ErrorMsg");
            }

            var result = _repo.Get();

            if(!result.IsSuccess)
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(new ApothecaryViewModel[] { });
            }
            else
            {
                var apothecaries = result.Value.Select(x => (ApothecaryViewModel)x);
                return View(apothecaries);
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ApothecaryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _repo.Add(model);

            if (result.IsSuccess) return RedirectToAction("Index");
            else
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(model);
            }
        }

        public IActionResult Fire(int id)
        {
            var result = _repo.Fire(id);
            var errorMsg = string.Empty;
            if (!result.IsSuccess) errorMsg = result.FailureMessage;

            return RedirectToIndex(errorMsg);
        }

        public IActionResult Details(int id)
        {
            var result = _repo.Get(id);

            if (result.IsSuccess)
            {
                return View((ApothecaryViewModel)result.Value);
            }
            else
            {
                return RedirectToIndex($"Nie ma aptekarza o id {id}");
            }
        }

        public IActionResult Edit(int id)
        {
            var result = _repo.Get(id);
            if (result.IsSuccess)
            {
                return View((ApothecaryViewModel)result.Value);
            }
            else
            {
                return RedirectToIndex(result.FailureMessage);
            }
        }

        [HttpPost]
        public IActionResult Edit(ApothecaryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = _repo.Update(model);

            if (result.IsSuccess)
            {
                return RedirectToAction("Details", new { id = result.Value.Id});
            }
            else
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            var result = _repo.Remove(id);
            var errorMsg = string.Empty;

            if (!result.IsSuccess) errorMsg = result.FailureMessage;

            return RedirectToIndex(errorMsg);
        }
    }
}
