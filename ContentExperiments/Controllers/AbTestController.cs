using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ContentExperiments.WebUI.Models;
using Microsoft.AspNetCore.Cors;

namespace ContentExperiments.Controllers
{
    [Route("api/[controller]")]
    public class ABTestController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ABTestController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("[action]")]
        public JavaScriptResult GetJs(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;

            string result = System.IO.File.ReadAllText(webRootPath + "/JavaScript.js");
            using (var context = new ABContext())
            {
                var abtest = context.ABTests.FirstOrDefault(x => x.Id == id);
                result = result.Replace("@selector", "'" + abtest.Selector + "'");
                result = result.Replace("@htmlA", "'" + abtest.HtmlA + "'");
                result = result.Replace("@htmlB", "'" + abtest.HtmlB + "'");
                result = result.Replace("@pageviews", abtest.PageViews.ToString());
                result = result.Replace("@id", abtest.Id.ToString());
            }

            return new JavaScriptResult(result);
        }
        [HttpGet("[action]")]
        public IActionResult RegisterPageviews(int pageviews, int id)
        {
            using (var context = new ABContext())
            {
                var abtest = context.ABTests.FirstOrDefault(x => x.Id == id);
                abtest.PageViews = pageviews;
                context.SaveChanges();
            }
            return Content("Pageviews updated");
        }
        [HttpGet("[action]")]
        public IActionResult Click(string state, int id)
        {
            using (var context = new ABContext())
            {
                var abtest = context.ABTests.FirstOrDefault(x => x.Id == id);
                switch (state)
                {
                    case "A": abtest.ClicksA++; break;
                    case "B": abtest.ClicksB++; break;
                }
                context.SaveChanges();
            }
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
