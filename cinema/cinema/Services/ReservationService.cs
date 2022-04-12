using cinema.Data;
using cinema.Models;

namespace cinema.Services
{
    public class ReservationService : IReservationService
    {
        private readonly CinemaContext _context;
        public ReservationService(CinemaContext context)
        {
            _context = context;
        }

        public List<TicketOrder> GetReservations()
        {
            return _context.Orders.ToList();
        }
    }
}
