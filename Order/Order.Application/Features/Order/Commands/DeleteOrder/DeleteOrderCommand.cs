using MediatR;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteOrderCommand: IRequest<IResponse>
    {
        public Guid OrderiId { get; set; }
        public DeleteOrderCommand(Guid OrderiId)
        {
            OrderiId = OrderiId;    
        }
    }
}
