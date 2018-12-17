using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.DrugStoreStockModels
{
    public class DrugStoreStockViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id Leku")]
        public int MedicineId { get; set; }

        [Display(Name = "Ilość leku na stanie")]
        public int Quantity { get; set; }

        public MedicineViewModel Medicine { get; set; }
    }
}
