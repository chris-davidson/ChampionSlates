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

        private static async Task<Response> CreateDatabase(string connectionString, string dbname)
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
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }
            return response;
        }

        private static async Task<Response> RunCreate(NpgsqlConnection connection, CreateSql createSql)
        {
            var response = new Response();
            try
            {
                using (var cmd = new NpgsqlCommand(createSql.SqlForCreate, connection))
                {
                    await connection.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }

                response.Message = $"| {createSql.TableName} creation executed. ";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Error = $" {createSql.TableName} | {ex.Message} ";
            }
            return response;
        }

        private static async Task<Response> CreateTables(string connectionString, string dbname)
        {
            var response = new Response { Success = true };
            try
            {
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString)
                {
                    Database = dbname
                };

                using (var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString))
                {
                    foreach (CreateSql tableSql in Statements.TableList)
                    {
                        var createResponse = await RunCreate(connection, tableSql);
                        response.Message += createResponse.Message;
                        response.Error += createResponse.Error;
                        if (!createResponse.Success) 
                        { 
                            response.Success = false; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Error += ex.Message;
                response.Success = false;
            }
            return response;
        }

        public static async Task<Response> InitDb(string connectionString, string dbname)
        {
            var response = CreateDatabase(connectionString, dbname).Result;
            var tablesResponse = await CreateTables(connectionString, dbname);
            response.Message += tablesResponse.Message;
            response.Error += tablesResponse.Error;
            response.Success = tablesResponse.Success;
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
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
                    response.Message = $"PostgreSql {connection.Host}:{connection.Port} connection opened.";
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
