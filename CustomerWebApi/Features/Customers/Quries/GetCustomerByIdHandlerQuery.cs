using AutoMapper;
using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Helpers;
using MediatR;

namespace CustomerWebApi.Features.Customers.Quries
{
    public class GetCustomerByIdHandlerQuery : IRequestHandler<GetCustomerById, IResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandlerQuery(ICustomerRepository customerRepository, IMapper mapper)
        {
            this._customerRepository = customerRepository;
            this._mapper = mapper;
        }
        public async Task<IResponse> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerById(request.id);
            return customer;
        }

    }
}
