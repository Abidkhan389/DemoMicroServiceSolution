using Identity.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Quries
{
    public class GetUserById:IRequest<IResponse>
    {
        public Guid id { get; set; }
        //public GetUserById(Guid ProductId)
        //{
        //    this.id = ProductId;
        //}
    }
}
