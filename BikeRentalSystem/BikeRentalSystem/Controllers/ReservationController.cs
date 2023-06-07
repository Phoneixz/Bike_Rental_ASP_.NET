using AutoMapper;
using AutoMapper.QueryableExtensions;
using BikeRentalSystem.Infrastructure.Database;
using BikeRentalSystem.Models;
using BikeRentalSystem.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BikeRentalSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public ReservationController(ReservationRepository reservationRepository, IMapper mapper,
               VehicleRepository vehicleRepository, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var reservations = _reservationRepository.GetAll().ToList();
            var reservationViewModels = _mapper.Map<List<Reservation>,List<RentalPointDetailViewModel>>(reservations);
            var reservationItemViewModel = new RentalPointItemViewModel
            {
                RentalPoints = reservationViewModels
            };
            return View(reservationItemViewModel);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var reservation = _reservationRepository.GetByID(id);
            var vehicle = _vehicleRepository.GetAll().Include(v => v.VehicleType);
            var vm = _mapper.Map<Reservation, ReservationDetailViewModel>(reservation);
            return View(vm);
        }
        public IActionResult Create()
        {
            var vehicles = _vehicleRepository.GetAll().ProjectTo<SelectListItem>(_mapper.ConfigurationProvider).ToList();
            var vm = new ReservationDetailViewModel { SelList = vehicles, PickupDate = DateTime.Now, ReturnDate = DateTime.Now };
            vm.TotalCost = 0;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationDetailViewModel reservationVm,[FromServices] IValidator<Reservation> reservationValidator)
        {
            double totalCost = 0;
            if (reservationVm.Vehicles != null && reservationVm.Vehicles.Any())
            {
                var selectedVehicleIds = reservationVm.SelectedVehicleIds;
                var selectedVehicles = reservationVm.Vehicles.Where(v => selectedVehicleIds.Contains(v.Id.ToString())).ToList();
                totalCost = selectedVehicles.Sum(vehicle => vehicle.Price);
            }
            var user = await _userManager.GetUserAsync(User);
            var reservation = new Reservation
            {
                PickUpDate = reservationVm.PickupDate,
                ReturnDate = reservationVm.ReturnDate,
                CustomerId = user.Id,
                Status = "Approved",
                Vehicles = reservationVm.Vehicles,
                TotalCost = totalCost
            };
            var ReservationValidation = reservationValidator.Validate(reservation);

            if(ReservationValidation.IsValid) 
            {
                _reservationRepository.Add(reservation);
                return RedirectToAction("Index","Home");
            }
            else
            {
                foreach(var e in ReservationValidation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                var updatedVm = new ReservationDetailViewModel
                {
                    Vehicles = reservationVm.Vehicles,
                    PickupDate = reservationVm.PickupDate,
                    ReturnDate = reservationVm.ReturnDate,
                    CustomerId = user.Id,
                    TotalCost = totalCost
                };
                return View("Create", updatedVm);
            }
        }

    }
}
