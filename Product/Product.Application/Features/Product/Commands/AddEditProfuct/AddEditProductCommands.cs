using MediatR;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.AddEditProfuct
{
    public class AddEditProductCommands: IRequest<IResponse>
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductPrice { get; set; }
    }
}
