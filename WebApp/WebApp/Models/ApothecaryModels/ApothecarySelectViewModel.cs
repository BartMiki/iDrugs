using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ApothecaryModels
{
    public class ApothecarySelectViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public static IEnumerable<ApothecarySelectViewModel> ToApothecarySelectViewModels(IEnumerable<ApothecaryViewModel> viewModels)
        {
            return Mapper.Map<IEnumerable<ApothecarySelectViewModel>>(viewModels);
        }
    }
}
