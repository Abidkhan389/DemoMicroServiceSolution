using Identity.Application.Helpers;
using Identity.Application.Helpers.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Quries
{
    public class GetUserList : TableParam, IRequest<IResponse>
    {
        public string? UserName { get; set; }
        public int? ActiveStatus { get; set; }
    }
}
