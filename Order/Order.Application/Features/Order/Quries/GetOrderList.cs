using MediatR;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Quries
{
    public class GetOrderList : IRequest<IResponse>
    {
        public GetOrderList()
        {

        }
    }
}
