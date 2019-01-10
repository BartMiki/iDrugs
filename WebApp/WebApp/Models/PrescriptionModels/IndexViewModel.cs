using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.PrescriptionModels
{
    public class IndexViewModel
    {
        public IEnumerable<PrescriptionViewModel> Prescriptions { get; set; }
        public int? SearchId { get; set; }
    }
}
