using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
//using Model.Models;

namespace Model
{
    internal class ApplicationDbContext : DbContext
    {
        //DbSet<IngridientData> IngridientData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=46.39.79.3\\DEV;User ID=user5;Password=5555;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }

}
