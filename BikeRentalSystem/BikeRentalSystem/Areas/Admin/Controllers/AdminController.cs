using Microsoft.AspNetCore.Mvc;

namespace BikeRentalSystem.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
