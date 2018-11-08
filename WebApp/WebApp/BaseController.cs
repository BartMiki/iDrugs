using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
