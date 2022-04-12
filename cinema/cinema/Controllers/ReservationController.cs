using cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            return View(_reservationService.GetReservations());
        }
    }
}
