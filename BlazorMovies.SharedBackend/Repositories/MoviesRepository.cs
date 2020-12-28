using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.SharedBackend.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DetailsMovieDTO> GetDetailMovieDTO(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IndexPageDTO> GetIndexPageDTO()
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
                UpComingReleases = upcomingReleases
            };

            return response;
        }

        public Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResponse<List<Movie>>> GetMoviesFiletered(FilterMoviesDTO filterMoviesDTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
