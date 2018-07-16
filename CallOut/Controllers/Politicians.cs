using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace CallOut.Controllers
{
    public class Politicians : Controller
    {
        [HttpGet("politicians/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
