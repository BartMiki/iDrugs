using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.PrescriptionModels;

namespace WebApp.Controllers
{
    public class PrescriptionController : BaseController
    {
        private readonly IPrescriptionRepo _prescriptionRepo;
        private readonly IApothecaryRepo _apothecaryRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly ILogger<PrescriptionController> _logger;

        public PrescriptionController(IPrescriptionRepo prescriptionRepo, 
            IApothecaryRepo apothecaryRepo, 
            IDoctorRepo doctorRepo, 
            ILogger<PrescriptionController> logger)
        {
            _prescriptionRepo = prescriptionRepo;
            _apothecaryRepo = apothecaryRepo;
            _doctorRepo = doctorRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            DisplayErrorFromRedirectIfNecessary();

            var result = _prescriptionRepo.Get();

            if (result.IsSuccess)
            {
                var model = Mapper.Map<IEnumerable<PrescriptionViewModel>>(result.Value);
                return View(model);
            }

            AddLocalError(result.FailureMessage);
            return View(Enumerable.Empty<PrescriptionViewModel>());
        }

        public IActionResult CreatePrescription()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CreatePrescription(PrescriptionViewModel model)
        {
            throw new NotImplementedException();
        }

        public IActionResult PrescriptionDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditPrescription(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult EditPrescription(PrescriptionViewModel model)
        {
            throw new NotImplementedException();
        }

        public IActionResult DeletePrescription(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult AddPrescriptionItem(int prescriptionId, int itemId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult AddPrescriptionItem(PrescriptionItemViewModel model)
        {
            throw new NotImplementedException();
        }

        public IActionResult RemovePrescriptionItem(int prescriptionId, int itemId)
        {
            throw new NotImplementedException();
        }

        public IActionResult EditPrescriptonItem(int prescriptionId, int itemId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult EditPrescriptonItem(PrescriptionItemViewModel model)
        {
            throw new NotImplementedException();
        }

        public IActionResult PrescriptionItemDetails(int prescriptionId, int itemId)
        {
            throw new NotImplementedException();
        }

        public IActionResult AcceptPrescription(int id)
        {
            throw new NotImplementedException();
        }
    }
}