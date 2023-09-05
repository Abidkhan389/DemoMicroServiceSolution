using Identity.Application.Contracts.Persistance;
using Identity.Application.Features.Identity.Commands.LoginUser;
using Identity.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Commands.LoginUser_
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IResponse>
    {
        private readonly IIdentityRepository _identityRepository;

        public LoginUserCommandHandler(IIdentityRepository identityRepository)
        {
            this._identityRepository = identityRepository?? throw new ArgumentNullException(nameof(identityRepository));
        }
        public async Task<IResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user= await _identityRepository.LoginUserAsync(request);
            return user;
        }
    }
}
