using JwtAuthenticationManager;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Persistance;
using Order.Infrastructure.Persistance;
using Order.Infrastructure.Repositories;
using System.Reflection;

namespace Order.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContextPool<OrderDbContext>(
            //options => options.UseNpgsql(configuration.GetConnectionString("OrderDbConnection")));
            services.AddDbContextPool<OrderDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("OrderDbConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Order.Migrations")
                );
            });
            // In your startup class or composition root
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<Order.Application.Helpers.IResponse, Order.Application.Helpers.Response>();
            services.AddCustomJwtAuthentication();
            services.AddMassTransit(x =>
            {
                // Configure MassTransit and RabbitMQ for the Order service.
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMQ:Uri"]), h =>
                    {
                        h.Username(configuration["RabbitMQ:UserName"]);
                        h.Password(configuration["RabbitMQ:Password"]);
                    });
                });
                // Add consumers specific to the Order service.
                //x.AddConsumer<OrderConsumer>();
            });

            services.AddMassTransitHostedService();

            return services;

        }
    }
}
