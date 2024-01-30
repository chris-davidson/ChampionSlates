using Microsoft.AspNetCore.Mvc;
using Models.Contracts;
using Models.DbCrud;

namespace DataService.Controllers
{
    public class DataRequestsController : ControllerBase
    {
        private readonly ICrudOperations _serviceCalls;

        public DataRequestsController(ICrudOperations _serviceCalls)
        {
            this._serviceCalls = _serviceCalls;
        }

        [HttpGet("Ping")]
        public async Task<ActionResult<Response>> Ping()
        {
            var response = await _serviceCalls.Ping();
            return Ok(response);
        }

        [HttpGet("CheckDb")]
        public async Task<ActionResult<Response>> CheckDb()
        {
            var response = await _serviceCalls.CheckDb();
            return Ok(response);
        }

        [HttpPost("Init")]
        public async Task<ActionResult<Response>> Init()
        {
            var response = await _serviceCalls.DatabaseInit();
            return Ok(response);
        }
    }
}
