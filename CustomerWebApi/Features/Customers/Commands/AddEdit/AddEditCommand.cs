using CustomerWebApi.Features.Customers.Quries;
using CustomerWebApi.Helpers;
using CustomerWebApi.Models;
using MediatR;

namespace CustomerWebApi.Features.Customers.Commands.AddEdit
{
    public class AddEditCommand :IRequest<IResponse>
    {
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
