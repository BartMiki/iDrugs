using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public MedicineTypeViewModel MedicineType { get; set; }
        public decimal? Refund { get; set; }
        public string Name { get; set; }
        public bool Expired { get; set; }
    }
}
