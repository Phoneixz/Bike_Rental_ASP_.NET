using Microsoft.AspNetCore.Mvc;
using BikeRentalSystem.Models;
using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.ViewModels;

namespace BikeRentalSystem.Controllers
{

    public class VehiclesController : Controller
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly AppDbContext _context;
        public VehiclesController(IRepository<Vehicle> vehicleRepository, AppDbContext appDbContext)
        {
            _vehicleRepository = vehicleRepository;
            _context = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAll().Include(v => v.VehicleType);

            var vehicleViewModels = vehicles.Select(v => new VehicleDetailViewModel
            {
                Id = v.Id,
                Make = v.Make,
                Description = v.Description,
                Price = v.Price,
                ImgURL = v.ImgURL,
                VehicleType = v.VehicleType,
                Availability = v.Availability
            }).ToList();

            var vehicleItemViewModel = new VehicleItemViewModel
            {
                Vehicles = vehicleViewModels
            };
            return View(vehicleItemViewModel);
        }
        public IActionResult GetVehicleDetails(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id);
            var vehicleDetailViewModel = new VehicleDetailViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Description = vehicle.Description,
                Price = vehicle.Price,
                ImgURL = vehicle.ImgURL,
                VehicleType = vehicle.VehicleType,
                Availability = vehicle.Availability
            };
            return View(vehicleDetailViewModel);
        }
        public IActionResult Create()
        {
            var vehicleTypes = _context.VehicleTypes.ToList();
            var vm = new VehicleDetailViewModel { VehicleTypes = vehicleTypes };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateVehicle(VehicleDetailViewModel vm)
        {
            var vehicle = new Vehicle
            {
                Make = vm.Make,
                Description = vm.Description,
                Price = vm.Price,
                ImgURL = vm.ImgURL,
                VehicleType = vm.VehicleType,
                Availability = vm.Availability
            };
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
        [HttpGet]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id);
            return View(vehicle);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _vehicleRepository.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
