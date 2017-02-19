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
        public IActionResult HandleFormPost1()
        {



            FlashMessage.Queue("Example message");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult HandleFormPost2()
        {

            FlashMessage.Queue("Example message");
            return RedirectToAction("Index");
        }
    }
}

