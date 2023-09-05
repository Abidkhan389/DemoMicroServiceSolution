using AutoMapper;
using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Helpers;
using MediatR;

namespace CustomerWebApi.Features.Customers.Quries
{
    
    //public class GetCustomerListQueryHandler : IRequestHandler<GetCustomersList,List<VM_Customer>>
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomersList, IResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerListQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }
        public async Task<IResponse> Handle(GetCustomersList request, CancellationToken cancellationToken)
        {
            var customerlist = await _customerRepository.GetAllCustomer(request);
            return customerlist;

        }
    }
}
