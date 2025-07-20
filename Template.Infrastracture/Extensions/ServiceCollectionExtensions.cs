using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ReportsBackend.Application.Interfaces;
using ReportsBackend.Infrastracture.Data.Context;
using ReportsBackend.Infrastracture.Services.Identity;
using ReportsBackend.Infrastracture.Services;
using ReportsBackend.Infrastracture.Seeders;
using ReportsBackend.Domain.Interfaces;
using ReportsBackend.Infrastracture.Repositories;

namespace ReportsBackend.Infrastracture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddDbContext<ApplicationDbContext>(options => options.UseOracle(connectionString).EnableSensitiveDataLogging());
      


            // Add Repositories
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Add Jwt Authentication
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<ITokenService, JwtService>();

            // Add Password Hasher
            services.AddScoped<IPasswordHasher, PasswordHasher>();


            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));



        }
    }
}
