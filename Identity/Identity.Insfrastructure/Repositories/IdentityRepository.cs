using AutoMapper;
using Identity.Application.Contracts.Persistance;
using Identity.Application.Contracts.Security;
using Identity.Application.Features.Identity.Commands.LoginUser;
using Identity.Application.Features.Identity.Commands.RegisterUser;
using Identity.Application.Features.Identity.Quries;
using Identity.Application.Helpers;
using Identity.Domain.Entities;
using Identity.Insfrastructure.Persistance;
using Identity.Insfrastructure.Repositories.GeneralServices;
using JwtAuthenticationManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.Infrastructure.Repositories
{
    public class IdentityRepository: IIdentityRepository
    {
        private readonly IdentityUserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICountResponse _countResp;
        private readonly ICryptoService _crypto;
        private readonly JwtTokenHandler _jwtTokenHandler;
        public IdentityRepository(IdentityUserDbContext context, IMapper mapper,
            IResponse response, UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, JwtTokenHandler jwtTokenHandler,
            RoleManager<IdentityRole> roleManager, ICountResponse countResp
            , ICryptoService crypto)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = response;
            this._userManager = userManager;
            //this._hostingEnvironment = hostingEnvironment;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._countResp = countResp;
            this._crypto = crypto;
            this. _jwtTokenHandler = jwtTokenHandler;

        }
        public async Task<IResponse> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null )
            {
                //var role = await _roleManager.FindByIdAsync(user.Id);
                var roles = await _userManager.GetRolesAsync(new ApplicationUser { Id = user.Id });

                if (roles is null)
                {
                    _response.Success = Constants.ResponseFailure;
                    _response.Message = Constants.NotFound.Replace("{data}", "user");
                    return _response;
                }
                else
                {
                    _response.Success = Constants.ResponseFailure;
                    _response.Message = Constants.NotFound.Replace("{data}", "Please delete user first from role");
                    return _response;
                }
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.NotFound.Replace("{data}", "user");
                return _response;
            }
            _response.Data = user;
            _response.Success = Constants.ResponseSuccess;
            _response.Message = Constants.DataSaved;
            return _response;
        }
        public async Task<IResponse> GetUserById(GetUserById UserId)
        {
            var user= await(from main in _userManager.Users
                             join userdetail in _context.Userdetail on main.Id equals userdetail.UserId
                             where (userdetail.Status == 1  && main.Id == UserId.ToString())
                             select new VM_Users
                             {
                                 UserId= main.Id,
                                 MobileNumber= main.MobileNumber,
                                 Status= userdetail.Status,
                                 Email=main.Email,
                                 FullName=main.UserName
                                 
                             }).FirstOrDefaultAsync();
            //var user = await _userManager.FindByIdAsync(UserId.ToString());
            if(user == null)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.NotFound.Replace("{data}", "user");
                return _response;
            }
            // Get user roles
            var roles = await _userManager.GetRolesAsync(new ApplicationUser { Id = user.UserId });
            // Assign roles to the user object
            user.Roles = roles.ToList();
            _response.Data=user;
            _response.Success = Constants.ResponseSuccess;
            _response.Message = Constants.DataSaved;
            return _response;
        }

        public async Task<IResponse> RegisterUser(RegisterUserCommands model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var existUser = await _userManager.FindByEmailAsync(model.Email);
                if (existUser != null)
                {
                    _response.Message = Constants.Exists.Replace("{data}", "{User}");
                    _response.Success = Constants.ResponseFailure;
                    return _response;
                }

                var UserRoles = new List<IdentityRole>();
                foreach (var roleid in model.RoleIds)
                {
                    var role = await _roleManager.FindByIdAsync(roleid);
                    if (role != null)
                    {
                        UserRoles.Add(role);
                    }
                    else
                    {
                        // Handle the case where the role does not exist.
                        _response.Message = Constants.NotFound.Replace("{data}", "{role}");
                        _response.Success = Constants.ResponseFailure;
                        return _response;
                    }
                }

                // Create a new ApplicationUser
                var user = new ApplicationUser
                {
                    IsSuperAdmin = false,
                    MobileNumber = model.MobileNumber,
                    Email = model.Email,
                    UserName = model.FirstName + model.LastName
                };

                // Salt and hash the password
                var salt = _crypto.CreateSalt();
                user.PasswordSalt = salt;
                user.PasswordHash = _crypto.CreateKey(salt, model.Password);

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    _response.Message = Constants.NotFound.Replace("{data}", "{userObj}");
                    _response.Success = Constants.ResponseFailure;
                    return _response;
                }

                Userdetail userdetail = new Userdetail();
                userdetail.Initialize(user);
                await _context.Userdetail.AddAsync(userdetail);
                await _context.SaveChangesAsync();

                var rolesResult = await _userManager.AddToRolesAsync(user, model.RoleIds);

                await transaction.CommitAsync();
                _response.Success = Constants.ResponseSuccess;
                _response.Message = Constants.DataSaved;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.Success = Constants.ResponseFailure;
                return _response;
            }
        }


        public async Task<IResponse> GetAllUsers(GetUserList model)
        {
            //bool? bStatus = model?.ActiveStatus == 1 ? true : (model?.ActiveStatus == 0 ? false : (bool?)null);
            var userList = await (from user in _userManager.Users
                                  join userdetail in _context.Userdetail on user.Id equals userdetail.UserId
                                  where (userdetail.Status == model.ActiveStatus || model.ActiveStatus == null)
                                  select new
                                  {
                                      User = user, 
                                      UserDetail = userdetail
                                  })
                      .ToListAsync();

            var tasks = userList.Select(async item => new VM_Users
            {
                UserId = item.User.Id,
                MobileNumber = item.User.MobileNumber,
                FullName = item.User.UserName,
                Status = item.UserDetail.Status,
                Email = item.User.Email,
                Roles = (List<string>)await _userManager.GetRolesAsync(item.User) // fetching user roles using _usermanager.getrolesasync for each user
            }).ToList();
            var vmUsersList = (await Task.WhenAll(tasks)).AsQueryable();
            var count = vmUsersList.Count();
            var sorted = await HelperStatic.OrderBy(vmUsersList, model.SortEx, model.OrderEx == "desc").Skip(model.Start).Take(model.LimitEx).ToListAsync();
            foreach (var item in sorted)
            {
                item.TotalCount = count;
                item.SerialNo = ++model.Start;
            }
            _countResp.DataList = sorted;
            _countResp.TotalCount = sorted.Count > 0 ? sorted.First().TotalCount : 0;
            _response.Success = Constants.ResponseSuccess;
            _response.Data = _countResp;
            return _response;
        }

        public async Task<IResponse> LoginUserAsync(LoginUserCommand model)
        {
            var user= await _userManager.FindByEmailAsync(model.Email);
            //if(user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            if(user != null &&  this._crypto.CheckKey(user.PasswordHash,user.PasswordSalt,model.Password))
            {

                var userRoles= await _userManager.GetRolesAsync(user);
                List<string> rolesList = (List<string>)userRoles;
                var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(user, rolesList);
                #pragma warning disable CS8601 // Possible null reference assignment.
                _response.Data = authenticationResponse;
                #pragma warning restore CS8601 // Possible null reference assignment.
                _response.Success = Constants.ResponseSuccess;
                return _response;
            }
            else
            {
                _response.Message = Constants.NotFound.Replace("{data}", "{user}");
                _response.Success = Constants.ResponseFailure;
                return _response;
            }
            
        }
        //private async Task<string> ProcessFileUpload(RegisterUserCommands model)
        //{
        //    string uniquefilename = null;
        //    if (model.Photo != null)
        //    {
        //        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images/User/");
        //        uniquefilename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
        //        string filepath = Path.Combine(uploadsFolder, uniquefilename);
        //        using (var filestream = new FileStream(filepath, FileMode.Create))
        //        {
        //            model.Photo.CopyTo(filestream);
        //        }
        //    }
        //    return uniquefilename;
        //}
    }
}
