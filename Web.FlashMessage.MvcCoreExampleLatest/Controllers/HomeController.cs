﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.FlashMessage.MvcCoreExampleLatest.Models;
using Vereyon.Web;

namespace Web.FlashMessage.MvcCoreExampleLatest.Controllers
{
    public class HomeController : Controller
    {

        private IFlashMessage _flashMessage;

        public HomeController(IFlashMessage flashMessage)
        {
            _flashMessage = flashMessage;
        }

        public IActionResult Index()
        {
			
			_flashMessage.Info("This flash message is always set on the Index page");
            return View();
        }

        public IActionResult RedirectExample()
        {
            ViewData["Message"] = "Your application description page.";

            _flashMessage.Confirmation("Test message with title", "Title");
            _flashMessage.Confirmation("Test message");

            return RedirectToAction("Index");
        }

        public IActionResult Bootstrap3()
        {

            _flashMessage.Confirmation("Test message with title", "Title");
            _flashMessage.Info("Test message");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
