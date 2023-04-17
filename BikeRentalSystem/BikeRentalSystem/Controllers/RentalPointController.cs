using AutoMapper;
using BikeRentalSystem.Infrastructure.Database;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using FluentValidation;
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
        public IActionResult CreateRentalPoint(RentalPointDetailViewModel rentalPointvm, [FromServices] IValidator<RentalPoint> rentalPointValidator)
        {
            var rentalPoint = new RentalPoint
            {
                Name = rentalPointvm.Name,
                Address = rentalPointvm.Address
            };
            var RentalPointValidation = rentalPointValidator.Validate(rentalPoint);
            if (RentalPointValidation.IsValid)
            {
                _repository.Add(rentalPoint);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var e in RentalPointValidation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                var updatedVm = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
                return View("Create",updatedVm);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            var viewModel = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditRentalPoint(RentalPointDetailViewModel viewModel, [FromServices] IValidator<RentalPoint> RentalPointValidator)
        {
            var rentalPoint = new RentalPoint
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Address = viewModel.Address
            };
            var rpValidator = RentalPointValidator.Validate(rentalPoint);
            if ( rpValidator.IsValid)
            {
                _repository.Update(rentalPoint);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var e in rpValidator.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                var updatedVm = _mapper.Map<RentalPoint, RentalPointDetailViewModel>(rentalPoint);
                return View("Edit", updatedVm);
            }
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
