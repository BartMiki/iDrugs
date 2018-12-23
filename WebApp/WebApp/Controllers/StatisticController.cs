using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.StatisticModels;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace WebApp.Controllers
{
    public class StatisticController : Controller
    {
        private readonly iDrugsEntities _context;

        public StatisticController(iDrugsEntities context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new StatisticsModel();

            var x1 = Try(() => 
            {
                var id = 0;
                var text = "";

                var result = _context.Apothecaries
                    .Where(x => x.FireDate != null)
                    .OrderByDescending(x => x.FireDate)
                    .FirstOrDefault();

                id = result.Id;
                var a = result.FirstName.EndsWith("a") ? "a" : "";
                var ay = result.FirstName.EndsWith("a") ? "a" : "y";
                var apot = result.FirstName.EndsWith("a") ? "Aptekarka" : "Aptekarz";
                text = $"{apot} {result.FirstName} {result.LastName}, został{a} zwolnion{ay} {result.FireDate}";

                return (text, id);
            }, typeof(StatisticController));

            model.ApothecaryFiredRecently = x1.IsSuccess ? x1.Value : ("Brak informacji", 0);

            var x2 = Try(() =>
            {
                var id = 0;
                var text = "";

                var result = _context.Apothecaries
                    .Select(x => new { x.Id, x.FirstName, x.LastName, orders = x.Orders.Count })
                    .OrderByDescending(x => x.orders)
                    .FirstOrDefault();

                id = result.Id;
                var a = result.FirstName.EndsWith("a") ? "a" : "";
                var apot = result.FirstName.EndsWith("a") ? "Aptekarka" : "Aptekarz";
                text = $"{apot} {result.FirstName} {result.LastName}, stworzył{a} {result.orders} zamówień";

                return (text, id);
            }, typeof(StatisticController));

            model.ApothecaryThatOrderedTheMost = x2.IsSuccess ? x2.Value : ("Brak informacji", 0);

            var x3 = Try(() =>
            {
                var id = 0;
                var text = "";

                var result = _context.Apothecaries
                    .OrderByDescending(x => x.RowVersion)
                    .FirstOrDefault();

                id = result.Id;
                var a = result.FirstName.EndsWith("a") ? "a" : "";
                var ay = result.FirstName.EndsWith("a") ? "a" : "y";
                var apot = result.FirstName.EndsWith("a") ? "Aptekarka" : "Aptekarz";
                text = $"{apot} {result.FirstName} {result.LastName}, był{a} edytown{ay} {result.RowVersion-1} razy";

                return (text, id);
            }, typeof(StatisticController));

            model.ApothecaryWithMostEdits = x3.IsSuccess ? x3.Value : ("Brak informacji", 0);

            var x4 = Try(() =>
            {
                var id = 0;
                var text = "";

                var result = _context.Orders
                    .Where(x => x.OrderCreationDate != null && x.SendOrderDate != null)
                    .ToArray()
                    .Select(x => new {
                        x.Id,
                        firstName = x.Apothecary.FirstName,
                        lastName = x.Apothecary.LastName,
                        span = x.SendOrderDate.Value.Subtract(x.OrderCreationDate.Value),
                        x.SendOrderDate,
                        x.OrderCreationDate
                    })
                    .OrderByDescending(x => x.span)
                    .FirstOrDefault();

                id = result.Id;
                text = $"Zamówienie stworzone przez {result.firstName} {result.lastName}," +
                $" zostało utworzone {result.OrderCreationDate} oraz wysłane {result.SendOrderDate} co daje" +
                $" {result.span.Days} dni pomiędzy stworzeniem, a zrealizowaniem zamówienia";

                return (text, id);
            }, typeof(StatisticController));

            model.OrderWithBigestTimeSpanBetweenCreationAndSending = x4.IsSuccess ? x4.Value : ("Brak informacji", 0);

            var x5 = Try(() =>
            {
                var id = 0;
                var text = "";

                var result = _context.Orders
                    .Select(x => new {
                        x.Id,
                        firstName = x.Apothecary.FirstName,
                        lastName = x.Apothecary.LastName,
                        totalPrice = x.OrderItems.Sum(i => i.Quantity * i.Medicine.UnitPrice)
                    })
                    .OrderByDescending(x => x.totalPrice)
                    .FirstOrDefault();

                id = result.Id;
                text = $"Zamówienie stworzone przez {result.firstName} {result.lastName}," +
                $" zawierało leki, których sumaryczna wartość wynosi {result.totalPrice} zł";

                return (text, id);
            }, typeof(StatisticController));

            model.OrderContainingMostExpensiveSumOfMedicinesPrices = x5.IsSuccess ? x5.Value : ("Brak informacji", 0);

            return View(model);
        }
    }
}