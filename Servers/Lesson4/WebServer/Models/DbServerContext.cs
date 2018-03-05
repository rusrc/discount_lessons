using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models
{
    public class DbServerContext : IdentityDbContext<IdentityUser>
    {
        public DbServerContext(DbContextOptions<DbServerContext> options)
            : base(options)
        {
        }
    }
}
