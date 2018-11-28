using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ApothecaryModels;

namespace WebApp.Models.OrderModels
{
    public class OrderViewModel
    {
        [Display(Name = "Id zamówienia")]
        public int Id { get; set; }

        public ApothecaryViewModel Apothecary { get; set; }

        [Display(Name = "Zamawiający")]
        public string ApothecaryFullName { get; set; }

        [Display(Name = "Data utworzenia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderCreationDate { get; set; }

        [Display(Name = "Data zamówienia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? SendOrderDate { get; set; }
    }
}
