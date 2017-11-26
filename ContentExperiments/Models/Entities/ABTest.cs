using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentExperiments.WebUI.Models.Entities
{
    public class ABTest
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public string HtmlA { get; set; }
        public string HtmlB { get; set; }
        public int ClicksA { get; set; }
        public int ClicksB { get; set; }
        public int PageViews { get; set; }
        public ApplicationUser User { get; set; }
    }
}
