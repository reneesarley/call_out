using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallOut.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CallOut.Controllers
{
    public class OffensesController : Controller
    {
        [HttpGet("/offenses/offense-form")]
        public IActionResult Index()
        {
            return View(Politician.GetAll());
        }

        [HttpPost("/offenses/offense-form")]
        public IActionResult Index(string description, int politicianselect, int sexism, int racism, int misogyny, int homophobia)
        {
            List<int> typeList = new List<int>() { sexism, racism, misogyny, homophobia };
            List<int> resultList = new List<int>() { };
            Offense newOffense = new Offense(politicianselect, description);
            foreach (int num in typeList)
            {
                if (!(num == 0))
                {
                    resultList.Add(num);
                }
            }
            newOffense.Save(resultList);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/offenses/offenseTracker")]
        public IActionResult FilteredOffenses(int politicianSelect, int typeId)
        {
            List<object> model = new List<object>() { Politician.Find(politicianSelect), Offense.GetOffenses(politicianSelect, typeId) };
            return View(model); 
        }

    }
}
