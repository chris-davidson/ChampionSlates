using Dapper;
using Models.DbCrud;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Diagnostics;

namespace DataService.Services.MySql
{
    public class Actions
    {
        private static async Task<Response> CreateDatabase(string connectionString, string dbname) 
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
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
            }
            return response;
        }

        private static async Task<Response> RunCreate(MySqlConnection connection, CreateSql createSql)
        {
            var response = new Response();
            try
            {
                using (var cmd = new MySqlCommand(createSql.SqlForCreate, connection)) 
                {
                    Debug.WriteLine(cmd.CommandText);
                    await cmd.ExecuteNonQueryAsync();
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
                var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
                {
                    Database = dbname
                };

                using (var connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
                {
                    await connection.OpenAsync();

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
                response.Message += ex.Message;
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
