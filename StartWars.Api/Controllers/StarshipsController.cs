using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarWars.Application.Interfaces;

namespace StartWars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StarshipsController : ControllerBase, IDisposable
    {
        private readonly IStarshipsAppService _appService;

        public StarshipsController(IStarshipsAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string manufacturer = "")
        {
            var main = await _appService.GetStarships(manufacturer);

            return Ok(main);
        }

        public void Dispose()
        {
        }
    }
}
