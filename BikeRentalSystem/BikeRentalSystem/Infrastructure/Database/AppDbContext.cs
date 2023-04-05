using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<RentalPoint> RentalPoints { get; set; }
    }
}
