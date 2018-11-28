using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.OrderModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public MedicineViewModel Medicine { get; set; }
        [Display(Name = "Zamawiana ilość")]
        public int Quantity { get; set; }
    }
}
