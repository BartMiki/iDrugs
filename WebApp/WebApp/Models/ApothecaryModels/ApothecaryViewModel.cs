using AutoMapper;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WebApp.Models.ApothecaryModels
{
    public class ApothecaryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Miesięczne zarobki")]
        [DisplayFormat(DataFormatString = "{0:0.00} zł")]
        public decimal MonthlySalary { get; set; }

        [Display(Name = "Czy zatrudniony?")]
        public bool IsEmployed { get; set; }

        [Display(Name = "Data zatrudnienia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Data zwolnienia")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FireDate { get; set; }


        public static implicit operator Apothecary(ApothecaryViewModel model)
        {
            return Mapper.Map<Apothecary>(model);
        }

        public static IEnumerable<Apothecary> ToApothecaries(IEnumerable<ApothecaryViewModel> viewModels)
        {
            return Mapper.Map<IEnumerable<Apothecary>>(viewModels);
        }

        public static implicit operator ApothecaryViewModel(Apothecary value)
        {
            return Mapper.Map<ApothecaryViewModel>(value);
        }

        public static IEnumerable<ApothecaryViewModel> ToApothecaryViewModels(IEnumerable<Apothecary> apothecaries)
        {
            return Mapper.Map<IEnumerable<ApothecaryViewModel>>(apothecaries);
        }
    }
}
