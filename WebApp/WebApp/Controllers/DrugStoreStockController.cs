using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DrugStoreStockModels;

namespace WebApp.Controllers
{
    public class DrugStoreStockController : BaseController
    {
        private readonly IDrugStoreStockRepo _drugStoreRepo;
        private readonly ILogger<DrugStoreStockController> _logger;

        public DrugStoreStockController(IDrugStoreStockRepo drugStoreRepo, ILogger<DrugStoreStockController> logger)
        {
            _drugStoreRepo = drugStoreRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInfo($"Zapytanie do metody Index()");

            DisplayErrorFromRedirectIfNecessary();

            var result = _drugStoreRepo.Get();

            if(result.IsSuccess)
            {
                return View(Mapper.Map<IEnumerable<DrugStoreStockViewModel>>(result.Value));
            }

            AddLocalError(result.FailureMessage);
            return View(Enumerable.Empty<DrugStoreStockViewModel>());

        }
    }
}