using Microsoft.AspNetCore.Mvc;
using BikeRentalSystem.Models;
using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var vehicleTypes = _context.VehicleTypes.
                Select( vt => new SelectListItem
                {
                    Value = vt.Id.ToString(),
                    Text = vt.Type
                })
                .ToList();
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
                VehicleTypeID = vm.VehicleTypeId,
                Availability = vm.Availability
            };
            _vehicleRepository.Add(vehicle);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id);
            var vehicleTypes = _context.VehicleTypes.ToList();
            var vm = new VehicleDetailViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Description = vehicle.Description,
                Price = vehicle.Price,
                ImgURL = vehicle.ImgURL,
                VehicleTypeId = vehicle.VehicleTypeID,
                Availability = vehicle.Availability,
                VehicleTypes = vehicleTypes.Select(vt => new SelectListItem
                {
                    Value = vt.Id.ToString(),
                    Text = vt.Type
                })
                .ToList()
            };
            return View(vm);
        }
        [HttpPost]

        public IActionResult Edit(VehicleDetailViewModel vm)
        {
            var vehicle = new Vehicle
            {
                Id = vm.Id,
                Make = vm.Make,
                Description = vm.Description,
                Price = vm.Price,
                ImgURL = vm.ImgURL,
                VehicleTypeID = vm.VehicleTypeId,
                Availability = vm.Availability
            };
            _vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id);
            var vm = new VehicleDetailViewModel
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Description = vehicle.Description,
                Price = vehicle.Price,
                ImgURL = vehicle.ImgURL,
                Availability = vehicle.Availability,
                VehicleTypeId = vehicle.VehicleTypeID
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(VehicleDetailViewModel vm)
        {
            _vehicleRepository.Delete(vm.Id);
            return RedirectToAction("Index");
        }
        
    }
}
