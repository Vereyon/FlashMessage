using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vereyon.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Web.FlashMessage.MvcCoreExample.Controllers
{
    public class HomeController : Controller
    {

        public IFlashMessage FlashMessage { get; private set; }

        public HomeController(IFlashMessage flashMessage)
        {

            // Retrieve a handle to the flash message service via DI.
            FlashMessage = flashMessage;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Warning()
        {
            FlashMessage.Warning("Example warning message");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Info()
        {
            FlashMessage.Info("Example informational message");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Confirmation()
        {
            FlashMessage.Confirmation("Example comfirmation message");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Danger()
        {
            FlashMessage.Danger("Example danger message");
            return RedirectToAction("Index");
        }
    }
}

