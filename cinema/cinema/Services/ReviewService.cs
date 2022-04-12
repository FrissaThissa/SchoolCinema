using cinema.Data;
using cinema.Identity;
using cinema.Models;
using cinema.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public List<Review> GetAllReviews()
        {
            List<Review> reviews = _context.Reviews.Include(r => r.Movie).ToList();
            return reviews;
        }

        public List<Review> GetMovieReviews(Movie movie)
        {
            return _context.Reviews.Where(r => r.Movie == movie).Include(r => r.Movie).ToList();
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

        public Review GetReviewById(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
