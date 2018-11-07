using DAL;
using DAL.Interfaces;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    public class ApothecaryController : BaseController
    {
        private readonly IApothecaryRepo _repo;

        public ApothecaryController()
        {
            _repo = new ApothecaryEfRepo();
        }

        public IActionResult Index()
        {
            ViewBag.ErrorMsg = string.Empty;
            if (TempData.ContainsKey("ErrorMsg"))
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
                TempData.Remove("ErrorMsg");
            }

            return View(_repo.Get().Value);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Apothecary apothecary)
        {
            if (!ModelState.IsValid) return View(apothecary);

            var result = _repo.Add(apothecary);

            if (result.IsSuccess) return RedirectToAction("Index");
            else
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(apothecary);
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
                return View(result.Value);
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
                return View(result.Value);
            }
            else
            {
                return RedirectToIndex(result.FailureMessage);
            }
        }

        [HttpPost]
        public IActionResult Edit(Apothecary apothecary)
        {
            if (!ModelState.IsValid) return View(apothecary);

            var result = _repo.Update(apothecary);

            if (result.IsSuccess)
            {
                return RedirectToAction("Details", new { id = result.Value.Id});
            }
            else
            {
                ViewBag.ErrorMsg = result.FailureMessage;
                return View(apothecary);
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
