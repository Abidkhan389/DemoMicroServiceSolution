using Identity.Application.Features.Identity.Commands.LoginUser;
using Identity.Application.Features.Identity.Commands.RegisterUser;
using Identity.Application.Features.Identity.Quries;
using Identity.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Contracts.Persistance
{
    public interface IIdentityRepository
    {
       // Task<IResponse> GetAllProducts(GetProductList model);
        Task<IResponse> GetUserById(GetUserById UserId);
        Task<IResponse> LoginUserAsync(LoginUserCommand model);
        Task<IResponse> DeleteUser(Guid id);
        Task<IResponse> GetAllUsers(GetUserList model);
        Task<IResponse> RegisterUser(RegisterUserCommands model);
    }
}
