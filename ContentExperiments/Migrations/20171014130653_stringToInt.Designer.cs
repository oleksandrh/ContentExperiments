using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ContentExperiments.WebUI.Models;

namespace ContentExperiments.WebUI.Migrations
{
    [DbContext(typeof(ABContext))]
    [Migration("20171014130653_stringToInt")]
    partial class stringToInt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ABTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClicksA");

                    b.Property<int>("ClicksB");

                    b.Property<string>("HtmlA");

                    b.Property<string>("HtmlB");

                    b.Property<string>("Selector");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("ABTests");
                });
        }
    }
}
