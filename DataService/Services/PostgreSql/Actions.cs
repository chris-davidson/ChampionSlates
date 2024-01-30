using Dapper;
using Models.DbCrud;
using Npgsql;

namespace DataService.Services.PostgreSql
{
    public class Actions
    {
        private static async Task<bool> CheckDbExists(NpgsqlConnection connection, string dbname)
        {
            try
            {
                var sqlDbExists = string.Format(Statements.DbExists, dbname);
                return await connection.ExecuteScalarAsync<bool>(sqlDbExists);
            }
            catch (PostgresException pex)
            {
                if (pex.SqlState == "3D000") { return false; }
                throw pex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<string> RunCreate(NpgsqlConnection connection, CreateSql createSql)
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
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
                {
                    Database = dbname
                };

                using (var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString))
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
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var dbExists = await CheckDbExists(connection, dbname);
                    if (dbExists)
                    {
                        response.Message += $"| {dbname} already exists. ";
                    }
                    else
                    {
                        var createSql = string.Format(Statements.CreateDatabase, dbname);
                        await connection.ExecuteAsync(createSql);
                        response.Message += $"| {dbname} create executed. ";
                    }
                }
                var tablesResponse = CreateTables(connectionString, dbname);
                response.Message += tablesResponse.Result.Message;
                response.Success = true;
            }
            catch (PostgresException pex)
            {
                response.Message += pex.Message;
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
                Message = "Connection string for Postgres is empty.",
                Success = false
            };

            if (String.IsNullOrEmpty(connectionString)) return response;

            try
            {
                NpgsqlConnection connection;
                using (connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
                    response.Message = $"PostgreSql {connection.Host}:{connection.Port} connection opened.";
                    response.Success = true;
                }
            }
            catch (PostgresException pex)
            {
                response.Message = $@"{pex.Message}
                    {connectionString}";
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
