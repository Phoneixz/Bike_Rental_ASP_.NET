using BikeRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BikeRentalSystem.ViewModels;

namespace BikeRentalSystem.Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RentalPoint> RentalPoints { get; set; }
        public DbSet<BikeRentalSystem.ViewModels.RentalPointDetailViewModel>? RentalPointDetailViewModel { get; set; }
        public DbSet<BikeRentalSystem.ViewModels.ReservationDetailViewModel>? ReservationDetailViewModel { get; set; }
    }
}
