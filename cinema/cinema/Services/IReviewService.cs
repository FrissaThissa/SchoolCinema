using cinema.Identity;
using cinema.Models;

namespace cinema.Services
{
    public interface IReviewService
    {
        public void CreateReview(string moviename, string username, int rating, string comment);
        public List<Review> GetAllReviews();
        public List<Review> GetMovieReviews(Movie movie);
        public Review GetReviewById(int id);
        public void DeleteReview(Review review);
    }
}
