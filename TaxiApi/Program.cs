using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NLog.Web;
using TaxiApi.Entities;
using TaxiApi.Exceptions;
using TaxiApi.Middleware;
using TaxiApi.Services;
using TaxiApi.Controllers;
using TaxiApi.Models;
using TaxiApi.Models.Validators;

namespace TaxiApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddDbContext<TaxiDbContext>();
            builder.Services.AddScoped<TaxiSeeder>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeMiddleware>();
            builder.Host.UseNLog();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TaxiSeeder>();
            seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Taxi API"); 
            });

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
