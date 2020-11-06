using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public interface IMoviesRepository
    {
        Task<int> CreateMovie(Movie movie);
        Task DeleteMovie(int id);
        Task<DetailsMovieDTO> GetDetailMovieDTO(int id);
        Task<IndexPageDTO> GetIndexPageDTO();
        Task<MovieUpdateDTO> GetMovieForUpdate(int id);
        Task UpdateMovie(Movie movie);
    }
}
