using AutoMapper;
using CustomerWebApi.Contracts.Persistence.ICustomer;
using CustomerWebApi.Features.Customers.Quries;
using CustomerWebApi.Helpers;
using MediatR;

namespace CustomerWebApi.Features.Customers.Commands.AddEdit
{
    public class AddEditCommandHandler:IRequestHandler<AddEditCommand, IResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<AddEditCommandHandler> _logger;

        public AddEditCommandHandler(ICustomerRepository customerRepository,  ILogger<AddEditCommandHandler> logger)
        {
            this._customerRepository = customerRepository?? throw new ArgumentNullException(nameof(customerRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResponse> Handle(AddEditCommand request ,CancellationToken cancellationToken)
        {
           // var customerObj = _mapper.Map<VM_Customer>(request);

            var customerobj = await _customerRepository.AddEditCustomer(request);
            if (customerobj.Success == true)
            {
                if(request.CustomerId != null)
                    _logger.LogInformation($"Customer {request.CustomerId} is successfully Updated.");
                else
                    _logger.LogInformation($"Customer {request.CustomerId} is successfully Created.");
            }
            return customerobj;
        }

    }
}
