using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.DoctorModels;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.PrescriptionModels
{
    public class AddPrescriptionItemViewModel : PrescriptionItemViewModel
    {
        [Display(Name = "Lek")]
        public IEnumerable<MedicineSelectModel> MedicineList { get; set; }
    }
}
