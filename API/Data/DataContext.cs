using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //we need to add this configuration to the startup class so that we can inject DataContext in other parts 
    //of our application
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
    }
}