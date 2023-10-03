using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Models;
using CustomerWebApi.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi.Contracts.Persistence.Consumers
{
    public class CustomerDetailsRequestConsumer : IConsumer<Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerDbContext _context;

        public CustomerDetailsRequestConsumer(ICustomerRepository customerRepository, CustomerDbContext context)
        {
            this._customerRepository = customerRepository;
            this._context = context;
        }
        public async Task Consume(ConsumeContext<Customer> context)
        {
            var request = context.Message;

            // Retrieve customer details based on the request

            Customer customer = await _context.Customer.FindAsync(request.CustomerId);
            await context.RespondAsync<Customer>(
            customer != null
                ? new { Message = $"Customer with ID {request.CustomerId} not found." }
                : customer
                 );
        }
    }
}
