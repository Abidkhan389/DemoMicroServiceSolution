using Identity.Application.Contracts.Persistance;
using Identity.Application.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Identity.Quries
{
    public class GetUserByHandlerQuery : IRequestHandler<GetUserById, IResponse>
    {
        private readonly IIdentityRepository _identityRepository;

        public GetUserByHandlerQuery(IIdentityRepository identityRepository)
        {
            this._identityRepository = identityRepository?? throw new ArgumentNullException(nameof(identityRepository));
        }
        public async Task<IResponse> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _identityRepository.GetUserById(request);
            return user;
        }
    }
}
