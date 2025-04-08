using System.Text.Json.Serialization;
using NLog.Web;
using TaxiApi.Entities;
using TaxiApi.Exceptions;
using TaxiApi.Services;

namespace TaxiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddDbContext<TaxiDbContext>();
            builder.Services.AddScoped<TaxiSeeder>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Host.UseNLog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TaxiSeeder>();
            seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
