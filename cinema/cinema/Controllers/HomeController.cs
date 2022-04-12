using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cinema.Controllers;

public class HomeController : Controller
{
    
    private readonly IHomeRepository homeRepository;
    private readonly IAuthorizationService _authorizationService;

    public HomeController(IHomeRepository homeRepository, IAuthorizationService authorization)
    {
        this.homeRepository = homeRepository;
        _authorizationService = authorization;
    }

    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var isEmployee = await _authorizationService.AuthorizeAsync(User, "employee");
        if (isEmployee.Succeeded)
        {
            List<string[]> actions = new List<string[]>();

            //Voorstellingen
            actions.Add(new string[] {"Voorstellingen", "Shows", "Index"});
            //Films
            actions.Add(new string[] {"Films", "Movies", "Index"});
            //Reserveringen
            actions.Add(new string[] { "Reserveringen", "Reservation", "Index" });
            //Reviews
            actions.Add(new string[] { "Reviews", "Reviews", "Index" });
            //Gevonden voorwerpen
            actions.Add(new string[] { "Gevonden voorwerpen", "FoundItems", "Index" });

            var userManagement = await _authorizationService.AuthorizeAsync(User, "usermanagement");
            if (userManagement.Succeeded)
            {
                //Gebruikers
                actions.Add(new string[] { "Gebruikers", "User", "Index" });
                //Rollen
                actions.Add(new string[] { "Rollen", "Role", "Index" });
            }

            ViewBag.Actions = actions;

            return View("Employee");
        }

        DateTime movieWeek = DateTime.Now.AddDays(8);
        var movies  =  homeRepository.GetMoviesThatStartWithin8DaysSort();
        ViewBag.Movies = movies;
        return View();
    }

    public IActionResult Error(string error)
    {
        ViewBag.error = error;
        return View();
    }
}