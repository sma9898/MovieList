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
        private MovieListContext context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MovieListContext con)
        {
            _logger = logger;
            context = con;
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


                //Add to database
                context.Movies.Add(appResponse);
                context.SaveChanges();

                //TempStorage.AddMovie(appResponse);

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
            return View(context.Movies);
            //            return View(Repository.AllEmpoyees);

            //return View(TempStorage.Movies.Where(r => r.Title != "Independence Day"));
        }

        //Update/Edit movie information
        public IActionResult Update(string Title)
        {
            MovieResponse movie = context.Movies.Where(m => m.Title == Title).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Update(MovieResponse movie, string Title)
        {
            //Get the information
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Category = movie.Category;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Title = movie.Title;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Director = movie.Director;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Year = movie.Year;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Rating = movie.Rating;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Edited = movie.Edited;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().LentTo = movie.LentTo;
            context.Movies.Where(m => m.Title == Title).FirstOrDefault().Notes = movie.Notes;

            //Save to the database
            context.SaveChanges();

            return RedirectToAction("MovieList");
        }

        //Delete
        [HttpPost]
        public IActionResult Delete(string Title)
        {
            MovieResponse movie = context.Movies.Where(m => m.Title == Title).FirstOrDefault();
            context.Remove(movie);
            //Save to the database
            context.SaveChanges();

            return RedirectToAction("MovieList");
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
