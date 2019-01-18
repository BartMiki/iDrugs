using AutoMapper;
using DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.OrderModels
{
    public class AddOrderItemViewModel
    {
        [Display(Name = "Lek")]
        public int MedicineId { get; set; }
        public int OrderId { get; set; }
        public IEnumerable<MedicineSelectModel> MedicineList { get; set; }
        [Display(Name = "Zamawiana ilość")]
        public int Quantity { get; set; }

        public static implicit operator OrderItem(AddOrderItemViewModel model)
        {
            return Mapper.Map<OrderItem>(model);
        }
    }
}
