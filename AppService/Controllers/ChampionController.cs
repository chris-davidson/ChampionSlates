using Microsoft.AspNetCore.Mvc;

namespace AppService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChampionController : ControllerBase
    {
        [HttpGet(Name = "Ping")]
        public async Task<ActionResult<string>> Get()
        {
            return Ok("Is anybody there?");
        }
    }
}
