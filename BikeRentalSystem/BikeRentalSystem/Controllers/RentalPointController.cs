using AutoMapper;
using BikeRentalSystem.Infrastructure.Database;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalSystem.Controllers
{
    public class RentalPointController : Controller
    {
        private readonly RentalPointRepository _repository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        public RentalPointController(RentalPointRepository repository, VehicleRepository vehicleRepository, IMapper mapper)
        {
            _repository = repository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rentalPoints = _repository.GetAll().ToList();
            var rentalPointViewModels = _mapper.Map<List<RentalPoint>, List<RentalPointDetailViewModel>>(rentalPoints);
            var rentalPointItemViewModel = new RentalPointItemViewModel
            {
                RentalPoints = rentalPointViewModels
            };
            return View(rentalPointItemViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            var vehicles = _vehicleRepository.GetAll().Include(v => v.VehicleType);
            var viewModel = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
            viewModel.Vehicles = vehicles.Where(v=> v.RentalPointId==rentalPoint.Id);
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
            var rentalPoint = _repository.GetByID(id);
            var viewModel = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditRentalPoint(RentalPointDetailViewModel viewModel)
        {
            var rentalPoint = new RentalPoint
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Address = viewModel.Address
            };
            _repository.Update(rentalPoint);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            var vm = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
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
