using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; private set; }
    }
}
