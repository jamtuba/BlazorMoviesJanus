﻿using BlazorMovies.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Repositories
{
    public interface IGenreRepository
    {
        Task CreateGenre(Genre genre);
        Task<List<Genre>> GetGenres();
        Task<Genre> GetGenre(int id);
        Task UpdateGenre(Genre genre);
        Task DeleteGenre(int id);
    }
}
