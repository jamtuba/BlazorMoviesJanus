﻿using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public interface IMoviesRepository
    {
        Task<int> CreateMovie(Movie movie);
        Task<IndexPageDTO> GetIndexPageDTO();
    }
}
