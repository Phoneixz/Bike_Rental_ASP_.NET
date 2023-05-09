using BikeRentalSystem.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace BikeRentalSystem.Models
{
    public class DataInitialization
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitialization(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize(AppDbContext _context) 
        {
            
            var vehicletype1 = new VehicleType { Type = "Rower górski" };
            var vehicletype2 = new VehicleType { Type = "Hulajnoga" };
            var vehicletype3 = new VehicleType { Type = "Rower miejski" };
            _context.VehicleTypes.AddRange(vehicletype1, vehicletype2, vehicletype3);
            _context.SaveChanges();
            var vehicle1 = new Vehicle
            {
                Id = 1,
                Make = "Kross",
                Description = "Wytrzymały rower stworzony do wycieczek górskich",
                Price = 30,
                ImgURL = "https://p.turbosquid.com/ts-thumb/A7/Ed2YIb/5k/0preview/jpg/1631874679/600x600/fit_q87/21fb24671593244dc20efde36b346d2b6b983d1a/0preview.jpg",
                Availability = true,
                VehicleTypeID = vehicletype1.Id,
                VehicleType = vehicletype1
            };
            var vehicle2 = new Vehicle
            {
                Id = 2,
                Make = "Xiaomi",
                Description = "Hulajnoga elektryczna sprawdzająca się w miejskich przejażdżkach",
                Price = 35,
                ImgURL = "https://proline.pl/pic/mi-electric-scooter-1s_1.jpg",
                Availability = false,
                VehicleTypeID = vehicletype2.Id,
                VehicleType = vehicletype2
            };
            var vehicle3 = new Vehicle 
            {
                Id = 3,
                Make = "Embassy Bikes",
                Description = "Rower stworzony z myślą o jeździe po mieście, przydający się zwłaszcza przy drobnych zakupach",
                Price = 15,
                ImgURL = "https://embassybikes.com/1772-home_default/city-bike-baby-powder-28.jpg",
                Availability = false,
                VehicleTypeID = vehicletype3.Id,
                VehicleType = vehicletype3
            };
                _context.Vehicles.AddRange(vehicle1,vehicle2,vehicle3);
                _context.SaveChanges();

            var rentalPoints = new List<RentalPoint>
            {
                new RentalPoint { Id= 1, Name="Rental point 1", Address="Stojałowskiego 15"},
                new RentalPoint { Id= 2, Name="Rental point 2", Address="Piastowska 32"}
            };
            _context.RentalPoints.AddRange(rentalPoints);
            _context.SaveChanges();
            vehicle1.RentalPointId = 1;
            vehicle2.RentalPointId = 2;
            vehicle3.RentalPointId = 1;
            _context.SaveChanges();
        }
        public async Task InitializeUsersAsync()
        {
            // create roles if they don't exist
            var roleNames = new[] { "Administrator", "User", "Operator" };
            foreach (var roleName in roleNames)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // create users
            var user = new IdentityUser
            {
                UserName = "user@example.com",
                Email = "user@example.com"
            };
            var userPassword = "User123!";

            var userExists = await _userManager.FindByEmailAsync(user.Email);
            
            if (userExists == null)
            {
                var res = await _userManager.CreateAsync(user, userPassword);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
            
            var adminUser = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };
            var adminPassword = "Admin123!";
            var adminRole = "Administrator";
            var adminUserExists = await _userManager.FindByEmailAsync(adminUser.Email);
            if (adminUserExists == null)
            {
                var res = await _userManager.CreateAsync(adminUser, adminPassword);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }

            var operatorUser = new IdentityUser
            {
                UserName = "operator@example.com",
                Email = "operator@example.com"
            };
            var operatorPassword = "Operator123!";
            var operatorRole = "Operator";

            var operatorUserExists = await _userManager.FindByEmailAsync(operatorUser.Email);
            if (operatorUserExists == null)
            {
                var res = await _userManager.CreateAsync(operatorUser, operatorPassword);
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(operatorUser, operatorRole);
                }
            }
        }
    }
}