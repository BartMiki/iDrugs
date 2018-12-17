using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.WarehouseModels
{
    public class AddWarehouseItemViewModel
    {
        [Display(Name = "Lek")]
        public int MedicineId { get; set; }

        [Display(Name = "Dodawana ilość")]
        public int Quantity { get; set; }

        public IEnumerable<MedicineSelectModel> MedicineList { get; set; }
    }
}
