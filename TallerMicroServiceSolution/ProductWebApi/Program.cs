using Microsoft.EntityFrameworkCore;
using ProductWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure the HTTP request pipeline.

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");

/*
var dbHost = "localhost";
var dbName = "GestionUsuarios";
var dbPassword = "1234";
var dbUserName = "admin";


var connectionString = "Server=localhost;Database=Customer;User Id=admin;Password=1234;Encrypt=false";
*/
var connectionString = $"Server={dbHost};port=3306;Database={dbName};User=root;Password={dbPassword}";

builder.Services.AddDbContext<ProductDbContext>(options => options.UseMySQL(connectionString));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
