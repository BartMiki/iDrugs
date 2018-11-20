using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int ApothecaryId { get; set; }
        public DateTime OrderDate { get; set; }
        //public IEnumerable<> OrderItems { get; set; }
    }
}
