using DriveItAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Do zmiany
/*builder.Services.AddDbContext<CarRentalContext>(opt =>
    opt.UseInMemoryDatabase("CarRental"));*/
var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION_STRING") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<CarRentalContext>(options =>
	options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarRentalContext>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}



app.Run();
