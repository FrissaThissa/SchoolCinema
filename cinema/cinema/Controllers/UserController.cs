using cinema.Identity;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            CinemaIdentityUser user = userManager.FindByIdAsync(id).Result;
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
