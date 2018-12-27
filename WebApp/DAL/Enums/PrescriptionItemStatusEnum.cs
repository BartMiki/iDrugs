using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    public enum PrescriptionItemStatusEnum
    {
        [Display(Name = "Dodany")]
        Created,
        [Display(Name = "Do wykupienia")]
        In_Progress,
        [Display(Name = "Wykupiony")]
        Bought
    }
}
