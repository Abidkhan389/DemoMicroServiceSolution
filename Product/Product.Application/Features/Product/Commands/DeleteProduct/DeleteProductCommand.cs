using MediatR;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand: IRequest<IResponse>
    {
        public Guid ProductiId { get; set; }
        public DeleteProductCommand(Guid productiId)
        {
            ProductiId = productiId;    
        }
    }
}
