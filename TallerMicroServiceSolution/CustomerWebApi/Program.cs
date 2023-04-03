using CustomerWebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure the HTTP request pipeline.

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var dbUserName = "admin";
/*
var dbHost = "localhost";
var dbName = "GestionUsuarios";
var dbPassword = "1234";
var dbUserName = "admin";


var connectionString = "Server=localhost;Database=Customer;User Id=admin;Password=1234;Encrypt=false";
*/
var connectionString = $"Server={dbHost};Database={dbName};User Id=sa;Password={dbPassword};Encrypt=false";

builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
