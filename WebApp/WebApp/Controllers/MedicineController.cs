using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MedicineController : BaseController
    {

        public IActionResult Index()
        {
            //var result = _repo.Get();

            return View();
        }
    }
}