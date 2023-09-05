using MediatR;
using Order.Application.Contracts.Persistance;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Quries
{
    internal class GetOrderByIdHandlerQuery : IRequestHandler<GetOrderById, IResponse>
    {
        private readonly IOrderRepository _OrderRepository;

        public GetOrderByIdHandlerQuery(IOrderRepository OrderRepository)
        {
            this._OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository));
        }
        public async Task<IResponse> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var customer = await _OrderRepository.GetOrderById(request.id);
            return customer;
        }
    }
}
