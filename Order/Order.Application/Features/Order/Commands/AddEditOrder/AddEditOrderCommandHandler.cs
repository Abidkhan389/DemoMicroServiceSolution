using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistance;
using Order.Application.Features.Order.Commands.AddEditOrder;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Product.Commands.AddEditOrder
{
    internal class AddEditOrderCommandHandler : IRequestHandler<AddEditOrderCommands, IResponse>
    {
        private readonly ILogger<AddEditOrderCommandHandler> _logger;
        private readonly IOrderRepository _OrderRepository;

        public AddEditOrderCommandHandler(ILogger<AddEditOrderCommandHandler> logger, IOrderRepository OrderRepository)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository));  
        }
        public async Task<IResponse> Handle(AddEditOrderCommands request, CancellationToken cancellationToken)
        {
            var Orderobj = await _OrderRepository.AddEditOrder(request);
            if (Orderobj.Success == true)
            {
                if (request.OrderId != null)
                    _logger.LogInformation($"Customer {request.OrderId} is successfully Updated.");
                else
                    _logger.LogInformation($"Customer {request.OrderId} is successfully Created.");
            }
            return Orderobj;
        }
    }
}
