﻿using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
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
