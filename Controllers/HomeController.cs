using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] //For when page loads
        public IActionResult EnterMovies()
        {
            return View();
        }

        [HttpPost] //For when form is submitted
        public IActionResult EnterMovies(MovieResponse appResponse)
        {
            //Check validation and don't allow Indpendence Day into the movie list
            if (ModelState.IsValid) // && (appResponse.Title != "Independence Day"))
            {
                //Display Title entered in form
                //Debug.WriteLine("Title: " + appResponse.Title);

                TempStorage.AddMovie(appResponse);

                //Pass in form/model data
                return View("Confirmation", appResponse);
            }
            /*else if (ModelState.IsValid)
            {
                return View("Confirmation", appResponse);
                //"Independence Day does not count :)";
            }*/
            else
            {
                return View();
            }
        }


        //Action for the Movie List from the Confirmation page
        public IActionResult MovieList()
        {
            return View(TempStorage.Movies.Where(r => r.Title != "Independence Day"));
        }

        //For My Podcasts page
        public IActionResult MyPodcasts()
        {
            return View("MyPodcasts");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
