using cinema.Data;
using cinema.Models;

namespace cinema.Services
{
    public class FoundItemService : IFoundItemService
    {
        private readonly CinemaContext _context;

        public FoundItemService(CinemaContext context)
        {
            _context = context;
        }

        public List<FoundItem> GetFoundItems()
        {
            return _context.FoundItems.ToList();
        }

        public void CreateFoundItem(string description, string image)
        {
            FoundItem foundItem = new FoundItem();
            foundItem.Description = description != null ? description : "";
            foundItem.Image = image != null ? image : "";
            _context.FoundItems.Add(foundItem);
            _context.SaveChanges();
        }

        public void DeleteFoundItem(int id)
        {
            _context.FoundItems.Remove(GetFoundItemById(id));
            _context.SaveChanges();
        }

        public FoundItem GetFoundItemById(int id)
        {
            return _context.FoundItems.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
