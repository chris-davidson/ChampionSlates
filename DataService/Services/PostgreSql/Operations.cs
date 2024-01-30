using Models.Contracts;
using Models.DbCrud;
using Models.Shared;
using static DataService.Services.PostgreSql.Actions;

namespace DataService.Services.PostgreSql
{
    public class Operations : ICrudOperations
    {
        private string _connectionString;
        private readonly string _database;

        public Operations(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
        }

        public async Task<Response> DatabaseInit()
        {
            var response = await InitDb(_connectionString, _database);
            return response;
        }

        public async Task<Response> CheckDb()
        {
            var response = await ValidateDbConnectivity(_connectionString);
            return response;
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
    }
}
