using cinema.Models;
using cinema.Repositories;
using cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewService _reviewService;
        public ReviewsController(IMovieRepository movieRepository, IReviewService reviewService)
        {
            _movieRepository = movieRepository;
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            List<Review> reviews = _reviewService.GetAllReviews();
            return View(reviews);
        }

        // GET: Reviews/Create/Avatar
        [Authorize]
        public IActionResult Create(string id)
        {  
            Review review = new Review();
            review.Movie = _movieRepository.FindMovieById(id).Result;
            return View(review);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromForm] string moviename, [FromForm] int rating, [FromForm] string comment)
        {
            _reviewService.CreateReview(moviename, User.Identity.Name, rating, comment);
            return View("Thanks");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Review review = _reviewService.GetReviewById(id);
            _reviewService.DeleteReview(review);
            return RedirectToAction("Index");
        }
    }
}
