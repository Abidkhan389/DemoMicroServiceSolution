using Identity.Application.Contracts.Persistance;
using Identity.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommands, IResponse>
    {
        private readonly IIdentityRepository _identityRepository;

        public RegisterUserCommandHandler(IIdentityRepository identityRepository)
        {
            this._identityRepository = identityRepository?? throw new ArgumentNullException(nameof(identityRepository));
        }
        public Task<IResponse> Handle(RegisterUserCommands request, CancellationToken cancellationToken)
        {
            var register= _identityRepository.RegisterUser(request);
            return register;
        }
    }
}
