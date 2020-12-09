using BlazorMovies.Client.Helpers;
using BlazorMovies.Shared.DTOs;
using System;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService _httpService;
        private readonly string baseURL = "api/accounts";

        public AccountsRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var httpResponse = await _httpService.Post<UserInfo, UserToken>($"{baseURL}/create", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response;
        }

        public async Task<UserToken> Login(UserInfo userInfo)
        {
            var httpResponse = await _httpService.Post<UserInfo, UserToken>($"{baseURL}/login", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response;
        }
    }
}
