using FootballScoresApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballScoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonsService _seasonsService;
        private readonly ILogger<SeasonsController> _logger;

        public SeasonsController(ILogger<SeasonsController> logger, ISeasonsService seasonsService)
        {
            _logger = logger;
            _seasonsService = seasonsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _seasonsService.GetAll());
        }
    }
}
