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

namespace ContentExperiments.Controllers
{
    [Route("api/[controller]")]
    public class ABTestController : Controller
    {
        private readonly IABTestsService abTestsService;
        private readonly IABTestsRepository abTestsRepository;
        public ABTestController(IABTestsService abTestsService, IABTestsRepository abTestsRepository)
        {
            this.abTestsService = abTestsService;
            this.abTestsRepository = abTestsRepository;
        }
        [HttpGet("[action]")]
        public JavaScriptResult GetJs(string userId)
        {
            string result = abTestsService.GetJSForUser(userId);
            return new JavaScriptResult(result);
        }

        [HttpGet("[action]")]
        public IActionResult RegisterPageviews(int pageviews, int id)
        {
            var abTest = abTestsRepository.Get(id);
            abTest.PageViews = pageviews;
            abTestsRepository.Update(abTest);
            return Content("Pageviews updated");
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
