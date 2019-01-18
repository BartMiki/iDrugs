using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.StatisticModels
{
    public class StatisticsModel
    {
        [Display(Name = "Aptekarz tworzący najwięcej zamówień")]
        public (string text, int id) ApothecaryThatOrderedTheMost { get; set; }
        [Display(Name = "Najdroższe zamówienie")]
        public (string text, int id) OrderContainingMostExpensiveSumOfMedicinesPrices { get; set; }
        [Display(Name = "Ostatnio zwolniony aptekarz")]
        public (string text, int id) ApothecaryFiredRecently { get; set; }
        [Display(Name = "Aptekarz z największa liczbą edycji profilu")]
        public (string text, int id) ApothecaryWithMostEdits { get; set; }
        [Display(Name = "Zamówienie o najdłuższym czasie pomiędzy stworzeniem, a zamówieniem")]
        public (string text, int id) OrderWithBigestTimeSpanBetweenCreationAndSending { get; set; }
    }
}
