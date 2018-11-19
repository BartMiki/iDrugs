using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MedicineTypeViewModel
    {
        public int Id { get; set; }
        public Unit Unit { get; set; }
        public MedType MedType { get; set; }
    }
}
