using DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApp.Controllers
{
    public class WarehouseController : Controller
    {
        public IActionResult Index()
        {

            using (var db = new iDrugsEntities())
            {
                var warehause = db.MedicineWarehouses.ToList();
                return View(warehause);
            }
        }

        public IActionResult Edit(int id, int medicineId, int quantity)
        {
            return View(new MedicineWarehouse { Id = id, MedicineId = medicineId, Quantity = quantity });
        }

        [HttpPost]
        public IActionResult Edit(MedicineWarehouse warehouse)
        {
            if (!ModelState.IsValid) return View(warehouse);

            using (var db = new iDrugsEntities())
            {
                var toUpdate = db.MedicineWarehouses.Where(w => w.Id == warehouse.Id).FirstOrDefault();
                toUpdate.Quantity = warehouse.Quantity;
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}