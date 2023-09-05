using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Helpers;
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
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddMassTransitHostedService(); // This hosts the bus


            return services;

        }
    }
}
