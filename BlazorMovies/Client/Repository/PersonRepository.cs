using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using BlazorMovies.Shared.Entities;
using BlazorMovies.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHttpService _httpService;
        private string _url = "api/people";

        public PersonRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<PaginatedResponse<List<Person>>> GetPeople(PaginationDTO paginationDTO)
        {
            return await _httpService.GetHelper<List<Person>>(_url, paginationDTO);
        }

        public async Task<List<Person>> GetPeopleByName(string name)
        {
            var response = await _httpService.Get<List<Person>>($"{_url}/search/{name}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<Person> GetPersonById(int id)
        {
            return await _httpService.GetHelper<Person>($"{_url}/{id}");
        }

        public async Task CreatePerson(Person person)
        {
            var response = await _httpService.Post(_url, person);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdatePerson(Person person)
        {
            var response = await _httpService.Put(_url, person);
            if (!response.Success)
            {
                //var error = await response.GetBody();
                //Console.WriteLine("ERROR: " + error);
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeletePerson(int id)
        {
            var response = await _httpService.Delete($"{_url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
