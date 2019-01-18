using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    //CHECK (MedType IN ('SALVE','PILL','SYRUP','SPRAY', 'SOLUTION', 'LIQUID')),
    public enum MedType
    {
        [Display(Name = "Maść")]
        Salve,
        [Display(Name = "Tabletki")]
        Pill,
        [Display(Name = "Syrop")]
        Syrup,
        [Display(Name = "Sprej")]
        Spray,
        [Display(Name = "Roztwór")]
        Solution,
        [Display(Name = "Ciecz")]
        Liquid
    }
}
