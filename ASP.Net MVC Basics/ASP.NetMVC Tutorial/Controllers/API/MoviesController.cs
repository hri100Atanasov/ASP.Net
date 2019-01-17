using ASP.NetMVC_Tutorial.Dtos;
using ASP.NetMVC_Tutorial.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ASP.NetMVC_Tutorial.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
                return _context.Movies.Include(m => m.Genre)
                    .ToList().Select(Mapper.Map<Movie, MovieDto>);
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = Roles.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            var genre = _context.Genres.Single(g => g.Id == movie.GenreId);
            movie.Genre = genre;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = Roles.CanManageMovies)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var movieInDb = _context.Movies.Single(m => m.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.CanManageMovies)]
        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
