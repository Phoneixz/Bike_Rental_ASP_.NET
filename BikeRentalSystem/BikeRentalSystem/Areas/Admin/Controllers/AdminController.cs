using BikeRentalSystem.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _appDbContext;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            List<IdentityUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> AdminRoleSet(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            await _userManager.AddToRoleAsync(user, "Administrator");
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
