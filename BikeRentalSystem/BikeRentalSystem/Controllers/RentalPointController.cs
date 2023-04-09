using BikeRentalSystem.Infrastructure.Database;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeRentalSystem.Controllers
{
    public class RentalPointController : Controller
    {
        private readonly IRepository<RentalPoint> _repository;
        private readonly IRepository<Vehicle> _vehicleRepository;
        public RentalPointController(IRepository<RentalPoint> repository, IRepository<Vehicle> vehiclerepository)
        {
            _repository = repository;
            _vehicleRepository = vehiclerepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rentalPoints = _repository.GetAll()
                .Select(rp => new RentalPointDetailViewModel
                {
                    Id = rp.Id,
                    Name = rp.Name,
                    Address = rp.Address,
                    Vehicles = rp.vehicles
                        .Select(v => new VehicleDetailViewModel
                        {
                            Id = v.Id,
                            ImgURL = v.ImgURL,
                            Availability = v.Availability,
                            Description = v.Description,
                            Make = v.Make,
                            VehicleTypeId = v.VehicleTypeID,
                            Price = v.Price
                        }).ToList()
                });

            var viewModel = new RentalPointItemViewModel
            {
                RentalPoints = rentalPoints.ToList()
            };

            return View(viewModel);
        }
  
        [HttpGet]
        public IActionResult Details(int id)
        {
            var rentalPoint = _repository.GetByID(id, rp=> rp.vehicles);
            var viewModel = new RentalPointDetailViewModel
            {
                Id = rentalPoint.Id,
                Name = rentalPoint.Name,
                Address = rentalPoint.Address,
                Vehicles = rentalPoint.vehicles
                    .Select(v => new VehicleDetailViewModel
                    {
                        Id = v.Id,
                        ImgURL = v.ImgURL,
                        Availability = v.Availability,
                        Description = v.Description,
                        Make = v.Make,
                        VehicleType = v.VehicleType,
                        Price = v.Price
                    }).ToList()
            };
            return View(viewModel);
        }
        public IActionResult Create()
        {
            var vm = new RentalPointDetailViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateRentalPoint(RentalPointDetailViewModel rentalPointvm)
        {
            var rentalPoint = new RentalPoint
            {
                Name = rentalPointvm.Name,
                Address = rentalPointvm.Address
            };
            _repository.Add(rentalPoint);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rentalPoint = _repository.GetByID(id, rp => rp.vehicles);
            var viewModel = new RentalPointDetailViewModel
            {
                Id = rentalPoint.Id,
                Name = rentalPoint.Name,
                Address = rentalPoint.Address,
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditRentalPoint(RentalPointDetailViewModel viewModel)
        {
            var rentalPoint = _repository.GetByID(viewModel.Id);
            rentalPoint.Name = viewModel.Name;
            rentalPoint.Address = viewModel.Address;
            _repository.Update(rentalPoint);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            var vm = new RentalPointDetailViewModel
            {
                Id = rentalPoint.Id,
                Name = rentalPoint.Name,
                Address = rentalPoint.Address
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult DeleteRentalPoint(RentalPointDetailViewModel vm)
        {
            _repository.Delete(vm.Id);
            return RedirectToAction("Index");
        }
    }
}
