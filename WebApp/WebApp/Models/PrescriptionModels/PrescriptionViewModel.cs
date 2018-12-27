using Common.Utils;
using DAL;
using System.Linq;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.ApothecaryModels;
using WebApp.Models.DoctorModels;
using static AutoMapper.Mapper;

namespace WebApp.Models.PrescriptionModels
{
    public class PrescriptionViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "ID Lekarza")]
        public int DoctorId { get; set; }

        [Display(Name = "Lekarz")]
        public string DoctorFullName { get; set; }

        [Display(Name = "ID Aptekarza")]
        public int ApothecaryId { get; set; }

        [Display(Name = "Aptekarz")]
        public string ApothecaryFullName { get; set; }

        [Display(Name = "Data przepisania")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime PrescriptionDate { get; set; }

        [Display(Name = "Data wykupienia")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime? CompletionDate { get; set; }

        [Display(Name = "Status recepty")]
        public PrescriptionStatusEnum Status { get; set; }
        public string StatusString { get => Status.GetDisplayName(); }

        [Display(Name = "Całkowity koszt")]
        [DisplayFormat(DataFormatString = "{0:0.00} zł")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Pozostały koszt")]
        [DisplayFormat(DataFormatString = "{0:0.00} zł")]
        public decimal RemainingCost { get => PrescriptionItems?.Sum(x => x.RemainingCost) ?? 0; }

        [Display(Name = "Email klienta")]
        public string Email { get; set; }

        [Display(Name = "Wersja")]
        public int RowVersion { get; set; }

        [Display(Name = "Aptekarz")]
        public virtual ApothecaryViewModel Apothecary { get; set; }

        [Display(Name = "Lekarz")]
        public DoctorViewModel Doctor { get; set; }

        [Display(Name = "Leki na recepcie")]
        public IEnumerable<PrescriptionItemViewModel> PrescriptionItems { get; set; }

        public static implicit operator Prescription(PrescriptionViewModel model)
        {
            return Map<Prescription>(model);
        }

        public static implicit operator PrescriptionViewModel(Prescription entity)
        {
            return Map<PrescriptionViewModel>(entity);
        }
    }
}
