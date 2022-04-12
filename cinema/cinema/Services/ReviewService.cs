using cinema.Data;
using cinema.Identity;
using cinema.Models;
using cinema.Repositories;

namespace cinema.Services
{
    public class ReviewService : IReviewService
    {
        private readonly CinemaContext _context;
        private readonly IMovieRepository _movieRepository;
        public ReviewService(CinemaContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }

        public List<Review> GetMovieReviews(Movie movie)
        {
            return _context.Reviews.Where(r => r.Movie == movie).ToList();
        }

        public int GetMovieRating(Movie movie)
        {
            List<Review> reviews = GetMovieReviews(movie);
            if (reviews.Count() == 0)
                return 0;

            int rating = 0;
            foreach(Review review in reviews)
            {
                rating += review.Rating;
            }
            rating = rating / reviews.Count();
            return rating;
        }

        public void CreateReview(string moviename, string username, int rating, string comment)
        {
            Review review = new Review();
            Movie movie = _movieRepository.FindMovieById(moviename).Result;
            review.Movie = movie;
            review.Username = username;
            review.Rating = rating;
            review.Comment = comment;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            movie.Rating = GetMovieRating(movie);
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }
    }
}
