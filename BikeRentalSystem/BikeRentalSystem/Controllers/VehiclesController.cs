using Microsoft.AspNetCore.Mvc;
using BikeRentalSystem.Models;
using BikeRentalSystem.Infrastructure.Database;

namespace BikeRentalSystem.Controllers
{

    public class VehiclesController : Controller
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        public VehiclesController(IRepository<Vehicle> vehicleRepository)
        {
            vehicleRepository = _vehicleRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAll();
            return View(vehicles);
        }
        [HttpGet]
        public IActionResult GetVehicleDetails(int id)
        {
            var vehicle = vehicles.SingleOrDefault(x => x.Id == id);
            return View(vehicle);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateVehicle(Vehicle vehicle)
        {
            _vehicleRepository.Add(vehicle);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id);
            return View(vehicle);
        }
        [HttpPost]

        public IActionResult Edit(Vehicle vehicle)
        {
            _vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }
        [HttpDelete]
        public IActionResult DeleteVehicle(int id)
        {
            _vehicleRepository.Delete(id);
            return View();
        }
        
    }
}
