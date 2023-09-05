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
    internal class GetUserListQueryHandler : IRequestHandler<GetUserList, IResponse>
    {
        private readonly IIdentityRepository _identityRepository;

        public GetUserListQueryHandler(IIdentityRepository identityRepository)
        {
            this._identityRepository = identityRepository?? throw new ArgumentNullException(nameof(identityRepository));
        }
        public Task<IResponse> Handle(GetUserList request, CancellationToken cancellationToken)
        {
            var UserList = _identityRepository.GetAllUsers(request);
            return UserList;
        }
    }
}
