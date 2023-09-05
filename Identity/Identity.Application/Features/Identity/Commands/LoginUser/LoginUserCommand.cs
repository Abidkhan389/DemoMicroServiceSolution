using Identity.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Commands.LoginUser
{
    public class LoginUserCommand :IRequest<IResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
