using CustomerWebApi.Contracts.Persistence.Consumers;
using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Models;
using JwtAuthenticationManager;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Persistance;
using Order.Infrastructure.Persistance;
using Order.Infrastructure.Repositories;
using RabbitMq.Services.Services2;
using System.Reflection;

namespace Order.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Entity Framework Core with Npgsql for PostgreSQL
            services.AddDbContextPool<OrderDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("OrderDbConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Order.Migrations")
                );
            });

            // Register repositories and other services
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<Order.Application.Helpers.IResponse, Order.Application.Helpers.Response>();

            // Configure AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure JWT Authentication
            services.AddCustomJwtAuthentication();

            // Configure MassTransit for messaging with RabbitMQ
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CustomerConsumer>(); // Register the customer consumer for both add and edit
                x.AddConsumer<CustomerDetailsRequestConsumer>(); // Register the customer details request consumer

                x.UsingRabbitMq((context, cfg) =>
                {
                    // Configure RabbitMQ host and credentials from your configuration
                    cfg.Host(new Uri(configuration[BusConstants.RabbitMqUri]), h =>
                    {
                        h.Username(configuration[BusConstants.UserName]);
                        h.Password(configuration[BusConstants.Password]);
                    });
                    // Request client for customer operations (both add and edit)
                    x.AddRequestClient<Customer>();
                    cfg.ReceiveEndpoint("order-service-queue", e =>
                    {
                        e.ConfigureConsumer<CustomerConsumer>(context);
                        e.ConfigureConsumer<CustomerDetailsRequestConsumer>(context);
                    });
                });
            });

            // Uncomment the line below if you want MassTransit to be hosted as a service
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
