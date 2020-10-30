using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/movies";

        public MoviesRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IndexPageDTO> GetIndexPageDTO()
        {
            return await Get<IndexPageDTO>(url); 
        }

        public async Task<DetailsMovieDTO> GetDetailMovieDTO(int id)
        {
            return await Get<DetailsMovieDTO>($"{url}/{id}");
        }

        private async Task<T> Get<T>(string url)
        {
            var response = await _httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<int> CreateMovie(Movie movie)
        {
            var response = await _httpService.Post<Movie, int>(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
