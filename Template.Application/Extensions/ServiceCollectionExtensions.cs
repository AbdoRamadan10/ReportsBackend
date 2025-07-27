using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Application.Interfaces;

using ReportsBackend.Application.Services;

namespace ReportsBackend.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var applicationAssemply = typeof(ServiceCollectionExtensions).Assembly;
            services.AddAutoMapper(applicationAssemply);

            services.AddScoped<ScreenService>();
            services.AddScoped<RoleService>();
            services.AddScoped<ReportService>();
            services.AddScoped<UserService>();


        }

    }
}
