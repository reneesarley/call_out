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
        public IActionResult Index(string description, int politician_select, int sexism, int racism, int misogyny, int homophobia)
        {
            List<int> typeList = new List<int>() { sexism, racism, misogyny, homophobia };
            List<int> resultList = new List<int>() { };
            Offense newOffense = new Offense(politician_select, description);
            foreach (int num in typeList)
            {
                if (!(num == 0))
                {
                    resultList.Add(num);
                }
            }
            newOffense.Save(resultList);
            return View("Index");
        }
    }
}
