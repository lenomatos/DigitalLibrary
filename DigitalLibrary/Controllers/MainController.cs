using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Controllers
{
    public class MainController : Controller
    {

        [TempData]
        public string ErrorMessage { get; set; }

        protected RedirectToActionResult ErrorMessageRedirect(
            string errorMessage = "Algo deu errado!",
            string actionName = "Index",
            string controllerName = "Home")
        {
            ErrorMessage = errorMessage;
            ModelState.AddModelError("", ErrorMessage);
            TempData["ErrorMessage"] = ErrorMessage;
            return RedirectToAction(actionName, controllerName);
        }
    }
}
