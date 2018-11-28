using AutoMapper;
using Common.Utils;
using DAL;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.MedicineModels
{
    public class CreateMedicineViewModel
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena całkowita")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Ilość")]
        public int Amount { get; set; }

        [Display(Name = "Jednostka")]
        public Unit Unit { get; set; }

        [Display(Name = "Rodzaj leku")]
        public MedType MedType { get; set; }

        [Display(Name = "Refundacja")]
        public decimal? Refund { get; set; }

        public IEnumerable<MedTypeSelectListItem> MedTypeSelectList { get => MedTypeSelectListItem.GetSelectList();  }

        public IEnumerable<UnitSelectListItem> UnitSelectList { get => UnitSelectListItem.GetSelectList(); }

        public static implicit operator Medicine(CreateMedicineViewModel model)
        {
            return Mapper.Map<Medicine>(model);
        }
    }
}
