using CustomerWebApi.Contracts.Persistence.Consumers;
using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Helpers;
using CustomerWebApi.Models;
using CustomerWebApi.Persistence;
using JwtAuthenticationManager;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerWebApi
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<CustomerDbContext>(
options     => options.UseNpgsql(configuration.GetConnectionString("CustomerDbConnection")));
            // In your startup class or composition root
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IResponse, Helpers.Response>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
           // services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddCustomJwtAuthentication();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CustomerDetailsRequestConsumer>(); // Register the consumer
                x.AddConsumer<CustomerConsumer>(); // Register the customer consumer for both add and edit
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    // Request clients for customer update and create responses
                    x.AddRequestClient<Customer>();
                    cfg.ReceiveEndpoint("customer-service-queue", e =>
                    {
                        e.ConfigureConsumer<CustomerDetailsRequestConsumer>(context);
                        e.ConfigureConsumer<CustomerConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService(); // This hosts the bus


            return services;

        }
    }
}
