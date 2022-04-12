using cinema.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    public class UserController : Controller
    {
        UserManager<CinemaIdentityUser> userManager;
        public UserController(UserManager<CinemaIdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }
    }
}
