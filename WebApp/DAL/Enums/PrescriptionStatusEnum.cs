using System.ComponentModel.DataAnnotations;

namespace DAL.Enums
{
    public enum PrescriptionStatusEnum
    {
        [Display(Name = "Dodana")]
        Created,
        [Display(Name = "W realizacji")]
        In_Progress,
        [Display(Name = "Zakończona")]
        Completed
    }
}
