using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.DoctorModels;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.PrescriptionModels
{
    public class AddPrescriptionItemViewModel : PrescriptionItemViewModel
    {
        public IEnumerable<MedicineSelectModel> DoctorSelect { get; set; }
    }
}
