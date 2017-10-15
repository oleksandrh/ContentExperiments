using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ContentExperiments.WebUI.Models;

namespace ContentExperiments.WebUI.Models
{
    public class ABContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ABTest> ABTests { get; set; }
        public ABContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}

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