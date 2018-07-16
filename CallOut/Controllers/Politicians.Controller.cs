using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallOut.Models;
using Microsoft.AspNetCore.Mvc;


namespace CallOut.Controllers
{
    public class PoliticiansController : Controller
    {
        [HttpGet ("politicians/index")]
        public IActionResult Index()
        {
            return View(Politician.GetAll());
        }

        [HttpGet("politicians/addNew")]
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost("politicians/addNew")]
        public IActionResult Save(string firstName, string lastName)
        {
            Politician newPolitician = new Politician(firstName, lastName);
            newPolitician.Save();

            return RedirectToAction("Index", "Politicians");
        }
    }
}
