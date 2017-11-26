using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ContentExperiments.WebUI.Models;
using ContentExperiments.WebUI.Models.Entities;

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

