using Dapper;
using Models.DbCrud;
using MySql.Data.MySqlClient;

namespace DataService.Services.MySql
{
    public class Actions
    {
        private static async Task<string> RunCreate(MySqlConnection connection, CreateSql createSql)
        {
            var response = $"| {createSql.TableName}";
            try
            {
                await connection.ExecuteAsync(createSql.SqlForCreate);
                response += $" created or exists. ";
            }
            catch (Exception ex)
            {
                response += $"| {ex.Message}";
            }
            return response;
        }

        private static async Task<Response> CreateTables(string connectionString, string dbname)
        {
            var response = new Response();
            try
            {
                var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
                {
                    Database = dbname
                };

                using (var connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();

                    foreach (CreateSql tableSql in Statements.TableList)
                    {
                        response.Message += await RunCreate(connection, tableSql);
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }
            return response;
        }

        public static async Task<Response> InitDb(string connectionString, string dbname)
        {
            var response = new Response { Message = $"Request to create {dbname}. " };
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var createSql = string.Format(Statements.CreateDatabase, dbname);
                    await connection.ExecuteAsync(createSql);
                    response.Message += $"| {dbname} create (if not exists) executed. ";
                }
                var tablesResponse = CreateTables(connectionString, dbname);
                response.Message += tablesResponse.Result.Message;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }
            return response;
        }

        public static async Task<Response> ValidateDbConnectivity(string connectionString)
        {
            var response = new Response
            {
                Message = "Connection string for MySql is empty.",
                Success = false
            };

            if (string.IsNullOrEmpty(connectionString)) return response;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
                    response.Message = $"MySql {connection.DataSource}:{connection.ServerVersion} connection opened.";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = $@"{ex.Message}
                    {connectionString}";
            }
            return response;
        }
    }
}
