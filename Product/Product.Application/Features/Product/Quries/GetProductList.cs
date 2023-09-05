using MediatR;
using Product.Application.Helpers;
using Product.Application.Helpers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Quries
{
    public class GetProductList : TableParam, IRequest<IResponse>
    {
        public string? ProductName { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
