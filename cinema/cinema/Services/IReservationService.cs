using cinema.Models;

namespace cinema.Services
{
    public interface IReservationService
    {
        public List<TicketOrder> GetReservations();
    }
}
