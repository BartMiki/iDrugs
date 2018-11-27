using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    public abstract class BaseController : Controller
    {
        protected virtual IActionResult RedirectToIndex(string errorMsg)
        {
            if (!string.IsNullOrEmpty(errorMsg))
            {
                TempData["ErrorMsg"] = errorMsg;
            }
            return RedirectToAction("Index");
        }

        protected virtual void DisplayTempErrorIfNedded()
        {
            if (TempData.ContainsKey("ErrorMsg"))
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
                TempData.Remove("ErrorMsg");
            }
        }
    }
}
