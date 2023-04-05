using BikeRentalSystem.Infrastructure.Database;
using BikeRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalSystem.Controllers
{
    public class RentalPointController : Controller
    {
        private readonly IRepository<RentalPoint> _repository;
        public RentalPointController(IRepository<RentalPoint> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var points = _repository.GetAll();
            return View(points);
        }
        [HttpGet]

        public IActionResult Details(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            return View(rentalPoint);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRentalPoint(RentalPoint rentalPoint)
        {
            _repository.Add(rentalPoint);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            return View(rentalPoint);
        }
        [HttpPost]
        public IActionResult EditRentalPoint(RentalPoint rentalPoint)
        {
            _repository.Update(rentalPoint);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var rentalPoint = _repository.GetByID(id);
            return View(rentalPoint);
        }
        [HttpPost]
        public IActionResult DeleteRentalPoint(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
