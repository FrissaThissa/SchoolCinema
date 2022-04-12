using cinema.Models;
using cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    public class FoundItemsController : Controller
    {
        private readonly IFoundItemService _foundItemService;
        public FoundItemsController(IFoundItemService foundItemService)
        {
            _foundItemService = foundItemService;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<FoundItem> foundItems = _foundItemService.GetFoundItems();
            return View(foundItems);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new FoundItem());
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromForm] string description, [FromForm] string image)
        {
            _foundItemService.CreateFoundItem(description, image);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _foundItemService.DeleteFoundItem(id);
            return RedirectToAction("Index");
        }
    }
}
