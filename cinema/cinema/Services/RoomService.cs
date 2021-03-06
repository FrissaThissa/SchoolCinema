using cinema.Models;
using System.Text.Json;
using cinema.Repositories;

namespace cinema.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public int[] GetRoomTemplate(Room room)
        {
            return JsonSerializer.Deserialize<int[]>(room.Template.Setting);
        }

        public Room GetShowRoom(Show show)
        {
            return _roomRepository.findRoomByShow(show);
        }

        public int GetTotalSeatAmount(Room room)
        {
            int[] rows = JsonSerializer.Deserialize<int[]>(room.Template.Setting);
            int amount = 0;
            foreach (var row in rows)
                amount += row;
            return amount;
        }
    }
}
