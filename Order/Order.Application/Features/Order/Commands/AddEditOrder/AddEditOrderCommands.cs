using MediatR;
using Order.Application.Helpers;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Commands.AddEditOrder
{
    public class AddEditOrderCommands: IRequest<IResponse>
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; }
        public DateTime OrderedOn { get; set; }
        public List<OrdersDetails> OrdersDetails { get; set; }
    }
}
