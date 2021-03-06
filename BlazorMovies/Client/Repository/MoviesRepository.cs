﻿using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _httpService.GetHelper<IndexPageDTO>(url, includeToken: false);
        }

        public async Task<MovieUpdateDTO> GetMovieForUpdate(int id)
        {
            return await _httpService.GetHelper<MovieUpdateDTO>($"{url}/update/{id}");
        }

        public async Task<DetailsMovieDTO> GetDetailMovieDTO(int id)
        {
            return await _httpService.GetHelper<DetailsMovieDTO>($"{url}/{id}", includeToken: false);
        }

        public async Task<PaginatedResponse<List<Movie>>> GetMoviesFiletered(FilterMoviesDTO filterMoviesDTO)
        {
            var responseHTTP = await _httpService.Post<FilterMoviesDTO, List<Movie>>($"{url}/filter", filterMoviesDTO, includeToken: false);
            var totalAmountPages = int.Parse(responseHTTP.HttpResponseMessage.Headers.GetValues("totalAmountPages").FirstOrDefault());
            var paginatedResponse = new PaginatedResponse<List<Movie>>()
            {
                Response = responseHTTP.Response,
                TotalAmountPages = totalAmountPages
            };

            return paginatedResponse;
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

        public async Task UpdateMovie(Movie movie)
        {
            var response = await _httpService.Put(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteMovie(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
