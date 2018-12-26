using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.MedicineModels;

namespace WebApp.Models.PrescriptionModels
{
    public class PrescriptionItemViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "ID recepty")]
        public int PrescriptionId { get; set; }

        [Display(Name = "Ilość do wykupienia")]
        public int QuantityToBuy { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Ilość kupiona")]
        public int QuantityAlreadyBought { get; set; }

        [Display(Name = "Kupiono / Przepisano")]
        public string QuantityRatio { get => $"{QuantityAlreadyBought} / {QuantityToBuy}"; }

        [Display(Name = "Wykupiono")]
        public bool BoughtOut { get => QuantityToBuy == QuantityAlreadyBought; }

        [Display(Name = "Pozostało do kupienia")]
        public int RemainingToBought { get => QuantityToBuy - QuantityAlreadyBought; }

        [Display(Name = "ID leku")]
        public int MedicineId { get; set; }

        [Display(Name = "Wersja")]
        public int RowVersion { get; set; }
        
        [Display(Name = "Lek")]
        public MedicineViewModel Medicine { get; set; }

        [Display(Name = "Recepta")]
        public PrescriptionViewModel Prescription { get; set; }
    }
}
