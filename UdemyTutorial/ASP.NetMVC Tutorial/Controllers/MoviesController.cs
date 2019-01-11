using ASP.NetMVC_Tutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        public async Task<ActionResult> Index()
        {
            var movies = _contex.Movies.ToList();

            return await Task.Run(()=> View(movies));
        }

        public async Task<ActionResult> Details(int id)
        {
            var movie = _contex.Movies.SingleOrDefault(m=>m.Id==id);

            return await Task.Run(()=>View(movie));
        }
    }
}