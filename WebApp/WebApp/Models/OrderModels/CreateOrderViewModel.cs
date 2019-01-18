using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.ApothecaryModels;

namespace WebApp.Models.OrderModels
{
    public class CreateOrderViewModel
    {
        //public string Search { get; set; }
        public int SelectedId { get; set; }
        public IEnumerable<ApothecarySelectViewModel> ApothecaryList { get; set; }
    }
}
