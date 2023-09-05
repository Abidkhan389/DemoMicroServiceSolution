using Identity.Application.Contracts.Persistance;
using Identity.Application.Helpers;
using Identity.Domain.Entities;
using Identity.Insfrastructure.Persistance;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Contracts.Security;
using Identity.Insfrastructure.Repositories.CryptoService;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Identity.Insfrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContextPool<IdentityUserDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("IdentityDbConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Identity.Migrations")
                );
            });

            // In your startup class or composition root

            services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 4;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredUniqueChars = 1;
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;

                //Options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<IdentityUserDbContext>()
              .AddDefaultTokenProviders();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<ICryptoService, CryptoHelper>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IResponse, Response>();
            services.AddCustomJwtAuthentication();
            return services;

        }
    }
}
