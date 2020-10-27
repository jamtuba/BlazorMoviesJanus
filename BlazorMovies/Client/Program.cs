using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorMovies.Client.Helpers;
using BlazorMovies.Client.Repository;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Tewr.Blazor.FileReader;

namespace BlazorMovies.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions(); // Authorization System
            services.AddTransient<IRepository, RepositoryInMemory>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
        }
    }
}
