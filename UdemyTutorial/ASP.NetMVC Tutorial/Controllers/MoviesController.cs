﻿using ASP.NetMVC_Tutorial.Models;
using ASP.NetMVC_Tutorial.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace ASP.NetMVC_Tutorial.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _contex;

        public MoviesController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }

        // GET: Movies
        public  ActionResult Index()
        {
            var movies = _contex.Movies.Include(m=>m.Genre).ToList();

            return View(movies);
        }

        //public async Task<ActionResult> Details(int id)
        //{
        //    var movie = _contex.Movies.SingleOrDefault(m => m.Id == id);

        //    return await Task.Run(() => View(movie));
        //}

        public ActionResult MovieForm()
        {
            var genres = _contex.Genres.ToList();
            var viewModel = new NewMovieViewModel
            {
                Genres = genres
            };

            ViewBag.New = "New";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _contex.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _contex.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }

            _contex.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movei = _contex.Movies.Single(m => m.Id == id);
            var newMoveViewModel = new NewMovieViewModel
            {
                Movie = movei,
                Genres = _contex.Genres.ToList()
            };

            return View("MovieForm", newMoveViewModel);
        }
    }
}