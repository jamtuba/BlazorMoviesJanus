using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorageService _fileStorageService;

        public MoviesController(ApplicationDbContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<IndexPageDTO>> Get()
        {
            var limit = 6;

            var moviesInTheaters = await _context.Movies
                .Where(x => x.InTheaters).Take(limit)
                .OrderByDescending(x => x.ReleaseDate)
                .ToListAsync();

            var todaysDate = DateTime.Today;

            var upcomingReleases = await _context.Movies
                .Where(x => x.ReleaseDate > todaysDate)
                .OrderBy(x => x.ReleaseDate).Take(limit)
                .ToListAsync();

            var response = new IndexPageDTO()
            {
                InTheaters = moviesInTheaters,
                UpComingRealeases = upcomingReleases
            };

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailsMovieDTO>> Get(int id)
        {
            var movie = await _context.Movies.Where(x => x.Id == id)
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.MoviesActors).ThenInclude(x => x.Person)
                .FirstOrDefaultAsync();

            if(movie == null) { return NotFound(); }

            movie.MoviesActors = movie.MoviesActors.OrderBy(x => x.Order).ToList();

            var model = new DetailsMovieDTO();
            model.Movie = movie;
            model.Genres = movie.MoviesGenres.Select(x => x.Genre).ToList();
            model.Actors = movie.MoviesActors.Select(x => new Person
                {
                    Name = x.Person.Name,
                    Picture = x.Person.Picture,
                    Character = x.Person.Character,
                    Id = x.Person.Id
                }
            ).ToList();

            return model;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Movie movie)
        {
            if (!string.IsNullOrWhiteSpace(movie.Poster))
            {
                var poster = Convert.FromBase64String(movie.Poster);
                movie.Poster = await _fileStorageService.SaveFile(poster, "jpg", "movies");
            }

            if(movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }

            _context.Add(movie);
            await _context.SaveChangesAsync();

            return movie.Id;
        }
    }
}
