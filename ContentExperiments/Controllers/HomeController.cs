using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentExperiments.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using ContentExperiments.WebUI.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ContentExperiments.Controllers
{
    public class FieldsVM
    {
        public string modelA { get; set; }
        public string modelB { get; set; }
        public string url { get; set; }
        public string selector { get; set; }
    }
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IABTestsRepository abTestsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public HomeController(IABTestsRepository abTestsRepository, UserManager<ApplicationUser> userManager)
        {
            this.abTestsRepository = abTestsRepository;
            this.userManager = userManager;
        }
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
            abTestsRepository.Save(new ABTest
            {
                Selector = model.selector,
                HtmlA = model.modelA,
                HtmlB = model.modelB,
                Url = model.url,
                ClicksA = 0,
                ClicksB = 0,
                User = userManager.GetUserAsync(HttpContext.User).Result
            });
            return true;
        }
    }
}
