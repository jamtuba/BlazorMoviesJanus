using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();

            return person.Id;
        }
    }
}
