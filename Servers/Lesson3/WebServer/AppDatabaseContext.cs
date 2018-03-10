using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options)
           : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseSqlServer($"Data Source=localhost;Initial Catalog={nameof(AppDatabaseContext)};Integrated Security=True");

        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<User> Users { get; set; }

    }


    /*
     1. Context -  AppDatabaseCOntext, StoreDbContext
     2. Classes 
        User - UserTable
        Manager - ManagerTable
        Product -> ProductTable
     3. Edit starup
     */
}
