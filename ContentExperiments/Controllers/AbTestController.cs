using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ContentExperiments.WebUI.Models;
using Microsoft.AspNetCore.Cors;
using ContentExperiments.WebUI.Repositories;
using ContentExperiments.WebUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using ContentExperiments.WebUI.Models.Entities;

namespace ContentExperiments.Controllers
{
    [Route("api/[controller]")]
    public class ABTestController : Controller
    {
        private readonly IABTestsService abTestsService;
        private readonly IABTestsRepository abTestsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public ABTestController(IABTestsService abTestsService, IABTestsRepository abTestsRepository, UserManager<ApplicationUser> userManager)
        {
            this.abTestsService = abTestsService;
            this.abTestsRepository = abTestsRepository;
            this.userManager = userManager;
        }
        [HttpGet("[action]")]
        public string GetScript(string userId)
        {
            return $"<script src='http://localhost:14255/api/abtest/getjs?userId={HttpContext.Session.GetString("UserId")}'></script>";
        }
        [HttpGet("[action]")]
        public IEnumerable<ABTest> GetABTests()
        {
            return abTestsRepository.GetByUserId(HttpContext.Session.GetString("UserId"));
        }
        [HttpGet("[action]")]
        public void Delete(int id)
        {
            abTestsRepository.Remove(id);
        }
        [HttpGet("[action]")]
        public JavaScriptResult GetJs(string userId)
        {
            return new JavaScriptResult(abTestsService.GetJSForUser(userId));
        }

        [HttpGet("[action]")]
        public void RegisterPageviews(int pageviews, int id)
        {
            var abTest = abTestsRepository.Get(id);
            abTest.PageViews = pageviews;
            abTestsRepository.Update(abTest);
        }
        [HttpGet("[action]")]
        public IActionResult Click(string state, int id)
        {
            var abTest = abTestsRepository.Get(id);
            switch (state)
            {
                case "A": abTest.ClicksA++; break;
                case "B": abTest.ClicksB++; break;
            }
            abTestsRepository.Update(abTest);
            return Content("Pageviews updated");
        }

    }
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType = "application/javascript";
        }
    }
}
