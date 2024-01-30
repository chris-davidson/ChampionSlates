using Models.Contracts;
using Models.DbCrud;

using Models.Shared;

namespace DataService.Services.Basic
{
    public class Operations : ICrudOperations
    {
        private string _dbErrorMessage = string.Empty;
        private bool ValidateDbConnectivity()
        {
            return true;
        }

        public Task<Response> CheckDb()
        {
            var response = new Response
            {
                Message = ValidateDbConnectivity() ? PingLines.Statement : _dbErrorMessage,
                Success = true
            };
            return Task.FromResult(response);
        }

        public Task<Response> Ping()
        {
            var response = new Response
            {
                Message = PingLines.Detail,
                Success = true
            };
            return Task.FromResult(response);
        }

        public Task<Response> DatabaseInit()
        {
            throw new NotImplementedException();
        }
    }
}
