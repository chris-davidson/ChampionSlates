using Microsoft.AspNetCore.Mvc;
using Models.Contracts;
using Models.Poco;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlatesController : ControllerBase
    {
        private readonly IApiOperations _serviceCalls;

        public SlatesController(IApiOperations _serviceCalls) 
        {
            this._serviceCalls = _serviceCalls;
        }

        [HttpGet("Ping")]
        public async Task<ActionResult<ResponseDto>> Ping()
        {
            var response = await _serviceCalls.Ping();
            return Ok(response);
        }

        [HttpPost("CreateFaction")]
        public async Task<ActionResult<ResponseDataDto<FactionDto>>> CreateFaction(FactionDto faction)
        {
            var response = await _serviceCalls.RequestCreateFaction(faction.Name, faction.Abbr);
            return Ok(response);
        }

        [HttpPost("CreateAlignment")]
        public async Task<ActionResult<ResponseDataDto<FactionDto>>> CreateAlignment(FactionDto faction)
        {
            var response = await _serviceCalls.RequestCreateFaction(faction.Name, faction.Abbr);
            return Ok(response);
        }
    }
}
