using cinema.Identity;

namespace cinema.Services
{
    public interface IReviewService
    {
        public void CreateReview(string moviename, string username, int rating, string comment);

    }
}
