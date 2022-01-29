using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {

        }

        [HttpGet()]
        public new IActionResult NotFound()
        {
            return View();
        }

        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View("NotFound");
        }

        [Route("Error/{code:int}")]
        public IActionResult Error(int code)
        {
            // handle different codes or just return the default error view
            return View("NotFound");
        }
    }
}
