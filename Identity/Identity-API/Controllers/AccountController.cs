using Identity.Application.Features.Identity.Commands.LoginUser;
using Identity.Application.Features.Identity.Commands.RegisterUser;
using Identity.Application.Features.Identity.Quries;
using Identity.Application.Helpers;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Identity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponse _response;

        public AccountController(IMediator mediator, IResponse response)
        {
            this._mediator = mediator;
            this._response = response;
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<object> GetUserById(GetUserById UserId)
        {
            if(UserId == null)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            return await _mediator.Send(UserId);

        }
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<object> RegisterUser(RegisterUserCommands model)
        {
            if (!ModelState.IsValid)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            return await _mediator.Send(model);
        }
        [HttpPost]
        [Route("LoginUser")]
        public async Task<object> LoginUser(LoginUserCommand model)
        {
            if(!ModelState.IsValid)
            {
                _response.Success=Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            return await _mediator.Send(model);
        }
        [HttpPost]
        [Route("GetAllByProc")]
        public async Task<object> GetAllByProc(GetUserList model)
        {
            if (!ModelState.IsValid)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            return await _mediator.Send(model);
        }
    }
}
