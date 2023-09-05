using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistance;
using Order.Application.Helpers;

namespace Order.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, IResponse>
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository OrderRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            this._OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var Ordertobj = await _OrderRepository.DeleteOrder(request.OrderiId);
            if(Ordertobj.Success == true)
            {
                    _logger.LogInformation($"Order is successfully deleted.");
            }
            return Ordertobj;
        }
    }
}
