using AutoMapper;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.WarehouseModels
{
    public class WarehouseItemViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Id lekarstwa")]
        public int MedicineId { get; set; }

        [Display(Name = "Ilość w hurtowni")]
        public int Quantity { get; set; }

        public MedicineViewModel Medicine { get; set; }

        public static implicit operator MedicineWarehouse(WarehouseItemViewModel model)
        {
            return Mapper.Map<MedicineWarehouse>(model);
        }

        public static implicit operator WarehouseItemViewModel(MedicineWarehouse entity)
        {
            return Mapper.Map<WarehouseItemViewModel>(entity);
        }
    }
}
