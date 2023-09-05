using MediatR;
using Order.Application.Contracts.Persistance;
using Order.Application.Contracts.Persistance;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Quries
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderList, IResponse>
    {
        private readonly IOrderRepository _OrderRepository;

        public GetOrderListQueryHandler(IOrderRepository OrderRepository)
        {
            this._OrderRepository = OrderRepository ?? throw new ArgumentNullException(nameof(OrderRepository));
        }
        public async Task<IResponse> Handle(GetOrderList request, CancellationToken cancellationToken)
        {
            var productlist = await _OrderRepository.GetAllOrders();
            return productlist;
        }
    }
}
