using BlazorMovies.Shared.Entities;
using System.Collections.Generic;

namespace BlazorMovies.Shared.DTOs
{
    public class IndexPageDTO
    {
        public List<Movie> InTheaters { get; set; }
        public List<Movie> UpComingReleases { get; set; }
    }
}
