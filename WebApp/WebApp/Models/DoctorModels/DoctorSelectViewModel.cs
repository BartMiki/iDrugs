using AutoMapper;
using DAL;
using System.Collections.Generic;
using static AutoMapper.Mapper;

namespace WebApp.Models.DoctorModels
{
    public class DoctorSelectViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public static IEnumerable<DoctorSelectViewModel> ToDoctorSelectViewModels(IEnumerable<DoctorViewModel> viewModels) =>
            Map<IEnumerable<DoctorSelectViewModel>>(viewModels);

        public static IEnumerable<DoctorSelectViewModel> ToDoctorSelectViewModels(IEnumerable<Doctor> doctors) =>
            Map<IEnumerable<DoctorSelectViewModel>>(doctors);
    }
}
