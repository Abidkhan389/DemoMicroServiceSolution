using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Models;
using MassTransit;

namespace CustomerWebApi.Contracts.Persistence.Consumers
{
    public class CustomerConsumer : IConsumer<Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerConsumer(ICustomerRepository customerRepository )
        {
            this._customerRepository = customerRepository;
        }
        public async Task Consume(ConsumeContext<Customer> context)
        {
            try
            {
                var customer = context.Message;
                var response = await _customerRepository.AddEditCustomer(new AddEditCommand
                {
                    CustomerId = customer.CustomerId!= null? customer.CustomerId:null, // Set the appropriate properties here
                    CustomerName = customer.CustomerName,
                    MobileNumber = customer.MobileNumber,
                    Email = customer.Email,
                });
                if (response.Success)
                {
                    // Log or process the success as needed
                    Console.WriteLine($"Successfully added/updated customer: {customer.CustomerName}");
                    await context.RespondAsync<Customer>(new
                    {
                        Message = response.Message
                    }) ;
                }
                else
                {
                    // Handle the failure (e.g., log or take corrective action)
                    Console.WriteLine($"Failed to add/update customer: {customer.CustomerName}. Reason: {response.Message}");
                    await context.RespondAsync<Customer>(new
                    {
                        Message = response.Message
                    });
                }
            }
            catch (Exception ex)
            {
                await context.RespondAsync(ex.Message); 
            }
        }
    }
}
