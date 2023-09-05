using CustomerWebApi.Helpers;
using CustomerWebApi.Helpers.General;
using MediatR;

namespace CustomerWebApi.Features.Customers.Quries
{
    //public class GetCustomersList : TableParam,  IRequest<List<VM_Customer>>
    public class GetCustomersList : TableParam, IRequest<IResponse>
    {
        public string? CustomerName { get; set; }
        public GetCustomersList() { }

    }
}
