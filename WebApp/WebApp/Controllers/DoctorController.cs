using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

            if(!result.IsSuccess)
            {
                AddLocalError(result.FailureMessage);
                return View(Enumerable.Empty<DoctorViewModel>());
            }

            var model = Mapper.Map<IEnumerable<DoctorViewModel>>(result.Value);

            return View(model);
        }
    }
}