using DataService.Services;
using Models.Contracts;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

var dbProvider = config["Dbms"];
string databaseName = string.IsNullOrEmpty(dbProvider) ? string.Empty : dbProvider;
Console.WriteLine(databaseName);

var championDatabase = config["DatabaseName"] ?? string.Empty;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (databaseName.ToLower().Contains("postgre"))
{
    builder.Services.AddScoped<ICrudOperations>(serviceProvider =>
    {
        var connectionString = config?.GetConnectionString("Postgres") ?? string.Empty;
        return new DataService.Services.PostgreSql.Operations(connectionString, championDatabase);
    });
}

else if (databaseName.ToLower().Contains("mysql"))
{
    builder.Services.AddScoped<ICrudOperations>(serviceProvider =>
    {
        var connectionString = config?.GetConnectionString("MySql") ?? string.Empty;
        return new DataService.Services.MySql.Operations(connectionString, championDatabase);
    });
    //options.UseMySql(
    //        config.GetConnectionString("MySql"),
    //        new MySqlServerVersion(new Version())
    //    );
}
else if (databaseName.ToLower().Contains("sqlserver"))
{
    //options.UseSqlServer(
    //        config.GetConnectionString("SqlServer")
    //    );
}
else
{
    builder.Services.AddScoped<ICrudOperations, DataService.Services.Basic.Operations>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
