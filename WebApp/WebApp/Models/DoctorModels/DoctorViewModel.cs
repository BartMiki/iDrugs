using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.DoctorModels
{
    public class DoctorViewModel
    {
        [Display(Name = "Id Lekarza")]
        public int Id { get; set; }

        [Display(Name ="Imię")]
        public string FirstName { get; set; }

        [Display(Name ="Nazwisko")]
        public string LastName { get; set; }

        [Display(Name ="Licencja")]
        public bool HasValidMedicalLicense { get; set; }

        [Display(Name = "Liczba edycji")]
        public int RowVersion { get; set; }
    }
}
