using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services
{

    public class ApplictionDbContext : DbContext
    {
        public ApplictionDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }


    }

}

