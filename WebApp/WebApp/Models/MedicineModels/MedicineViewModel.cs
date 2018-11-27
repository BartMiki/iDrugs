using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cena jednostkowa")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "Ilość")]
        public int Amount { get; set; }
        public MedicineTypeViewModel MedicineType { get; set; }
        [Display(Name = "Refundacja")]
        public decimal? Refund { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Wycofany z użycia")]
        public bool Expired { get; set; }
        [Display(Name = "Zawartość opakowania")]
        public string MedicineDescription { get; set; }
    }
}
