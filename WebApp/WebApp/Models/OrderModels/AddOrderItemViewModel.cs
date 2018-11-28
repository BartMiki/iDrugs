using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.OrderModels
{
    public class AddOrderItemViewModel
    {
        public int MedicineId { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Lek")]
        public IEnumerable<MedicineSelectModel> MedicineList { get; set; }
        [Display(Name = "Zamawiana ilość")]
        public int Quantity { get; set; }

        public static implicit operator OrderItem(AddOrderItemViewModel model)
        {
            return Mapper.Map<OrderItem>(model);
        }
    }
}
