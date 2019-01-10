using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.PrescriptionModels
{
    public class BuySingleItemViewModel
    {
        [Display(Name = "Lek")]
        public string MedicineName { get; set; }

        [Display(Name = "Pozostało do kupienia")]
        public int RemainingAmount { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Dostępne na stanie")]
        public int Available { get; set; }

        [Display(Name = "Kupowana ilość")]
        public int Amount { get; set; }

        [Display(Name = "Cena jednostkowa")]
        public decimal UnitPrice { get; set; }
    }

    public class BuySomeItemsViewModel
    {
        public int PrescriptionId { get; set; }
        public IList<BuySingleItemViewModel> Items { get; set; }
    }
}
