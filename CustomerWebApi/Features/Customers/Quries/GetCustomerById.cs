using CustomerWebApi.Helpers;
using MediatR;

namespace CustomerWebApi.Features.Customers.Quries
{
    public class GetCustomerById : IRequest<IResponse>
    {
        public Guid id { get; set; }
        public GetCustomerById() { }

    }
}
