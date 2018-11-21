using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    //CHECK (Unit IN ('PCS','GRAMS','MILILITERS'))
    public enum Unit
    {
        [Display(Name = "Sztuk")]
        Pcs,
        [Display(Name = "Gramy")]
        Grams,
        [Display(Name = "Mililitry")]
        Mililiters
    }
}
