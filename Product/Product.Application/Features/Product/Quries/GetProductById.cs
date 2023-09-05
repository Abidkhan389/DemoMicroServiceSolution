using MediatR;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Quries
{
    public class GetProductById: IRequest<IResponse>
    {
        public Guid id { get; set; }
        public GetProductById(Guid ProductId)
        {
            this.id = ProductId;
        }
    }
}
