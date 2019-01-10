using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.DoctorModels;

namespace WebApp.Models.PrescriptionModels
{
    public class CreatePrescriptionViewModel : PrescriptionViewModel
    {
        [Display(Name = "Doktor wydający receptę")]
        public IEnumerable<DoctorSelectViewModel> DoctorSelect { get; set; }

        [Display(Name = "Aptekarz dodający receptę")]
        public IEnumerable<ApothecarySelectViewModel> ApothecarySelect { get; set; }
    }
}
