using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiApplication.Models
{
    public class WebapiDbContext : IdentityDbContext
    {
        public WebapiDbContext(DbContextOptions<WebapiDbContext> options)
            :base(options)
        {

        }
    }
}
