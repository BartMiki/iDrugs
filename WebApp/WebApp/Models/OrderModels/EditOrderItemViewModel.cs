using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.OrderModels
{
    public class EditOrderItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lek")]
        public string MedicineName { get; set; }

        public int MedicineId { get; set; }
        public int OrderId { get; set; }
        
        [Display(Name = "Zamawiana ilość")]
        public int Quantity { get; set; }

        public static implicit operator EditOrderItemViewModel(OrderItem entity)
        {
            return Mapper.Map<EditOrderItemViewModel>(entity);
        }

        public static implicit operator OrderItem(EditOrderItemViewModel model)
        {
            return Mapper.Map<OrderItem>(model);
        }
    }
}
