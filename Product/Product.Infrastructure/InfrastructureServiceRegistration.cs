using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Contracts.Persistance;
using Product.Application.Helpers;
using Product.Infrastructure.Persistance;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContextPool<ProductDbContext>(
            //options => options.UseNpgsql(configuration.GetConnectionString("ProductDbConnection")));
            services.AddDbContextPool<ProductDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("ProductDbConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("product.Migrations")
                );
            });
            // In your startup class or composition root
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IResponse, Response>();
            services.AddCustomJwtAuthentication();

            return services;

        }
    }
}
