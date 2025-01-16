using DriveItAPI.Data;
using DriveItAPI.Middleware;
using Microsoft.EntityFrameworkCore;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // TODO
        var connectionString = /*Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION_STRING") ??*/ builder.Configuration.GetConnectionString("DefaultConnection");
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

        List<String> APIKeys = [Environment.GetEnvironmentVariable("API_KEY_1"), Environment.GetEnvironmentVariable("API_KEY_2")];
       
        app.UseMiddleware<ApiKeyMiddleware>(APIKeys);

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
    }
}


