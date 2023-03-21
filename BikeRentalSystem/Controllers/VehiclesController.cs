using Microsoft.AspNetCore.Mvc;
using BikeRentalSystem.Models;
namespace BikeRentalSystem.Controllers
{

    public class VehiclesController : Controller
    {
        List<Vehicle> vehicles = new List<Vehicle>
        {
            new Vehicle {Id=1,Name="Rower",Description="Rower górski",Price=30, ImgURL="https://p.turbosquid.com/ts-thumb/A7/Ed2YIb/5k/0preview/jpg/1631874679/600x600/fit_q87/21fb24671593244dc20efde36b346d2b6b983d1a/0preview.jpg"},
            new Vehicle {Id=2,Name="Hulajnoga",Description="Hulajnoga elektryczna", Price=50, ImgURL="https://proline.pl/pic/mi-electric-scooter-1s_1.jpg"},
            new Vehicle {Id=3,Name="Rower",Description="Rower miejski", Price=15, ImgURL="https://embassybikes.com/1772-home_default/city-bike-baby-powder-28.jpg" },
        };
        [HttpGet]
        public IActionResult Index()
        {

            return View(vehicles);
        }
        [HttpGet]
        public IActionResult GetVehicleDetails(int id)
        {
            var vehicle = vehicles.SingleOrDefault(x => x.Id == id);
            return View(vehicle);
        }

        
    }
}
