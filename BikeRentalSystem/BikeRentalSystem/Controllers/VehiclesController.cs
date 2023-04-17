using Microsoft.AspNetCore.Mvc;
using BikeRentalSystem.Models;
using BikeRentalSystem.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using BikeRentalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;

namespace BikeRentalSystem.Controllers
{

    public class VehiclesController : Controller
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly RentalPointRepository _rentalPointRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public VehiclesController(VehicleRepository vehicleRepository, AppDbContext appDbContext, RentalPointRepository rentalPointRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _context = appDbContext;
            _rentalPointRepository = rentalPointRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehicles = _vehicleRepository.GetAll().Include(v => v.VehicleType).ToList();
            var vehicleViewModels = _mapper.Map<List<Vehicle>, List<VehicleDetailViewModel>>(vehicles);
            var vehicleItemViewModel = new VehicleItemViewModel
            {
                Vehicles = vehicleViewModels
            };
            return View(vehicleItemViewModel);
        }
        public IActionResult GetVehicleDetails(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id, e => e.VehicleType);
            var vehicleDetailViewModel = _mapper.Map<Vehicle, VehicleDetailViewModel>(vehicle);
            return View(vehicleDetailViewModel);
        }
        public IActionResult Create()
        {
            var vehicleTypes = _context.VehicleTypes.ToList();
            var selListItems = _mapper.Map<List<VehicleType>, List<SelectListItem>>(vehicleTypes);
            var rentalPoints = _rentalPointRepository.GetAll().
                ProjectTo<SelectListItem>(_mapper.ConfigurationProvider).ToList();
            var vm = new VehicleDetailViewModel { VehicleTypes = selListItems, RentalPoints = rentalPoints };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateVehicle(VehicleDetailViewModel vm, [FromServices] IValidator<Vehicle> vehicleValidator)
        {
            var vehicle = new Vehicle
            {
                Make = vm.Make,
                Description = vm.Description,
                Price = vm.Price,
                ImgURL = vm.ImgURL,
                VehicleTypeID = vm.VehicleTypeId,
                RentalPointId = vm.RentalPointId,
                Availability = vm.Availability
            };
            var VehicleValidation = vehicleValidator.Validate(vehicle);

            if (VehicleValidation.IsValid)
            {
                _vehicleRepository.Add(vehicle);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var e in VehicleValidation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                var vehicleTypes = _context.VehicleTypes.ToList();
                var selListItems = _mapper.Map<List<VehicleType>, List<SelectListItem>>(vehicleTypes);
                var rentalPoints = _rentalPointRepository.GetAll().
                    ProjectTo<SelectListItem>(_mapper.ConfigurationProvider).ToList();
                var updatedVm = new VehicleDetailViewModel
                {
                    VehicleTypes = selListItems,
                    RentalPoints = rentalPoints,
                    Make = vm.Make,
                    Description = vm.Description,
                    Price = vm.Price,
                    ImgURL = vm.ImgURL,
                    VehicleTypeId = vm.VehicleTypeId,
                    RentalPointId = vm.RentalPointId,
                    Availability = vm.Availability
                };
                return View("Create", updatedVm);
            }
        }
        [HttpGet]
        public IActionResult EditVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id, e => e.VehicleType);
            var vehicleTypes = _context.VehicleTypes.ToList();
            var selListItems = _mapper.Map<List<VehicleType>, List<SelectListItem>>(vehicleTypes);
            var rentalPoints = _rentalPointRepository.GetAll().
                ProjectTo<SelectListItem>(_mapper.ConfigurationProvider).ToList();
            var vm = _mapper.Map<Vehicle, VehicleDetailViewModel>(vehicle);
            vm.VehicleTypes = selListItems;
            vm.RentalPoints = rentalPoints;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(VehicleDetailViewModel vm, [FromServices] IValidator<Vehicle> vehicleValidator)
        {
            var vehicle = new Vehicle
            {
                Id = vm.Id,
                Make = vm.Make,
                Description = vm.Description,
                Price = vm.Price,
                ImgURL = vm.ImgURL,
                VehicleTypeID = vm.VehicleTypeId,
                RentalPointId = vm.RentalPointId,
                Availability = vm.Availability
            };
            var VehicleValidator = vehicleValidator.Validate(vehicle);

            if (VehicleValidator.IsValid)
            {
                _vehicleRepository.Update(vehicle);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var e in  VehicleValidator.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                var vehicleTypes = _context.VehicleTypes.ToList();
                var selListItems = _mapper.Map<List<VehicleType>, List<SelectListItem>>(vehicleTypes);
                var rentalPoints = _rentalPointRepository.GetAll().
                    ProjectTo<SelectListItem>(_mapper.ConfigurationProvider).ToList();
                var updatedVm = _mapper.Map<Vehicle, VehicleDetailViewModel>(vehicle);
                updatedVm.VehicleTypes = selListItems;
                updatedVm.RentalPoints = rentalPoints;
                return View("EditVehicle", updatedVm);
            }
        }
        [HttpGet]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _vehicleRepository.GetByID(id, e => e.VehicleType);
            var vm = _mapper.Map<Vehicle, VehicleDetailViewModel>(vehicle);
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
