using CustomerWebApi.Helpers;
using MediatR;

namespace CustomerWebApi.Features.Customers.Commands.deleteCustomer
{
    public class DeleteCustomerCommand : IRequest<IResponse>
    {
        public Guid CustomerId { get; set; }
    }
}
