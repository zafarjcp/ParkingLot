using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLot.Api.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/{0}")]
        public IActionResult HandleErrorCodes(int statusCode)
        {
            ViewBag.Message = "Sorry, the resource you are looking for does not exist.";
            return View("HandleErrorCodes");
        }
    }
}
