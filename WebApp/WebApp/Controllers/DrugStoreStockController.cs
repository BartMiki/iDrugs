using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.DrugStoreStockModels;

namespace WebApp.Controllers
{
    public class DrugStoreStockController : BaseController
    {
        private readonly IDrugStoreStockRepo _drugStoreRepo;

        public DrugStoreStockController(IDrugStoreStockRepo drugStoreRepo)
        {
            _drugStoreRepo = drugStoreRepo;
        }

        public IActionResult Index()
        {
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