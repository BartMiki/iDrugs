using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class MedicineController : BaseController
    {

        public IActionResult Index()
        {
            //var result = _repo.Get();

            return View();
        }
    }
}