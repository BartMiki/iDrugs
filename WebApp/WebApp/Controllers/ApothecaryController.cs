using Common.Interfaces;
using Common.Utils;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Models.ApothecaryModels;

namespace WebApp
{
    public class ApothecaryController : BaseController
    {
        private readonly IApothecaryRepo _repo;
        private readonly ILogger<ApothecaryController> _logger;

        public ApothecaryController(IApothecaryRepo repo, ILogger<ApothecaryController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInfo($"Zapytanie do metody Index");

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
            DisplayErrorFromRedirectIfNecessary();

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
                AddErrorForRedirect(result.FailureMessage);
                return RedirectToAction("Edit", new { id = model.Id });
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
