using System.Configuration;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

            //Authentication settings
            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

            builder.Services.AddSingleton(authenticationSettings);

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddFluentValidationAutoValidation();
            //Database
            builder.Services.AddDbContext<TaxiDbContext>();
            builder.Services.AddScoped<TaxiSeeder>();
            //Automapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //Service interfaces
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            //Middleware
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeMiddleware>();
            //Nlog
            builder.Host.UseNLog();
            //Swagger
            builder.Services.AddSwaggerGen();
            //Hash password and validator
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<TaxiSeeder>();
            seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseAuthentication();

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
