using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System.Configuration;

namespace ContentExperiments.WebUI.Models
{
    public class ABContext : DbContext
    {
        public DbSet<ABTest> ABTests { get; set; }
        public ABContext() { }
        public ABContext(DbContextOptions<ABContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=abtesting; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
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
}