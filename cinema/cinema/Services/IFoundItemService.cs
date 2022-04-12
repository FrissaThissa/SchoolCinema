using cinema.Models;

namespace cinema.Services
{
    public interface IFoundItemService
    {
        public List<FoundItem> GetFoundItems();
        public void CreateFoundItem(string description, string image);
        public void DeleteFoundItem(int id);
    }
}
