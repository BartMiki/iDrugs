using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    /// <summary>
    /// Wrapper around standard Controller, containing usefull methods
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Redirect to Index
        /// </summary>
        protected virtual IActionResult RedirectToIndex()
        {
            return RedirectToIndex(null);
        }

        /// <summary>
        /// Redirect to Index with given Error Message
        /// </summary>
        protected virtual IActionResult RedirectToIndex(string errorMsg)
        {
            if (!string.IsNullOrEmpty(errorMsg))
            {
                TempData["ErrorMsg"] = errorMsg;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retrives and displays failure message from action that returned instance of Redirect... 
        /// </summary>
        protected virtual void DisplayErrorFromRedirectIfNecessary()
        {
            if (TempData.ContainsKey("ErrorMsg"))
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
                TempData.Remove("ErrorMsg");
            }
        }

        /// <summary>
        /// Use when error occured and action will return instance of Redirect...
        /// </summary>
        protected virtual void AddErrorForRedirect(string errorMsg)
        {
            TempData["ErrorMsg"] = errorMsg;
        }

        /// <summary>
        /// Use when error occured and action will return instance of View
        /// </summary>
        protected virtual void AddLocalError(string errorMsg)
        {
            ViewBag.ErrorMsg = errorMsg;
        }
    }
}
