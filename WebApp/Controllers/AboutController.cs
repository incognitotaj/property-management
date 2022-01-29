using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AboutController : Controller
    {
        private readonly IConfigurationRoot _config;

        public AboutController(IConfigurationRoot config)
        {
            _config = config;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            var model = new AboutViewModel
            {
                OfficeLocation = _config["Property:OfficeLocation"],
                Address = _config["Property:Address"],
                Lat = _config["Property:Location:Lat"],
                Lng = _config["Property:Location:Lng"],
            };

            return View(model);
        }
    }
}
