using AutoMapper;
using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Helpers;
using CustomerWebApi.Models;
using MediatR;

namespace CustomerWebApi.Features.Customers.Commands.deleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, IResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository,IMapper mapper, ILogger<DeleteCustomerCommandHandler> logger)
        {
            this._customerRepository = customerRepository?? throw new ArgumentNullException(nameof(customerRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerobj= await _customerRepository.GetCustomerById(request.CustomerId);
            if (customerobj != null)
            {
                var customerdeleteobject = _mapper.Map<Customer>(customerobj.Data);
                await _customerRepository.DeleteCustomer(customerdeleteobject);
                _logger.LogInformation($"Customer {customerdeleteobject.CustomerId} is successfully deleted.");

            }

            return customerobj;
        }
    }
}
