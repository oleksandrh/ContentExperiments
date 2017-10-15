using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentExperiments.WebUI.Models;

namespace ContentExperiments.Controllers
{
    public class Fields
    {
        public string selectedElementSelector { get; set; }
        public string selectedElementHtml { get; set; }
    }
    public class FieldsVM
    {
        public Fields modelA { get; set; }
        public Fields modelB { get; set; }
        public string page { get; set; }
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public bool Save(FieldsVM model)
        {
            using (var context = new ABContext())
            {
                context.ABTests.Add(new ABTest
                {
                    Selector = model.modelA.selectedElementSelector,
                    HtmlA = model.modelA.selectedElementHtml,
                    HtmlB = model.modelB.selectedElementHtml,
                    Url = model.page,
                    ClicksA = 0,
                    ClicksB = 0
                });
                context.SaveChanges();
            }
            return true;
        }
    }
}
