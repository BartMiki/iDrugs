using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class MedicineTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Jednostka")]
        public Unit Unit { get; set; }
        [Display(Name = "Rodzaj leku")]
        public MedType MedType { get; set; }
    }
}
