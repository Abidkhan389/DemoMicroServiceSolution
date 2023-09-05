using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Features.Customers.Quries;
using CustomerWebApi.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponse _response;

        public CustomerController(IMediator mediator, IResponse response)
        {
            this._mediator = mediator;
            this._response = response;
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllCustomer")]
        public async Task<object> GetAllCustomer([FromBody] GetCustomersList model)
        {
            if (!ModelState.IsValid)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            //var query = new GetCustomersList();
            return await _mediator.Send(model);
            
        }
        [HttpGet("{customerId}")]
        //[Route("GetCustomerById/{CustomerId}")]
        public async Task<object> GetCustomerById(Guid customerId)
        {
            if(customerId == Guid.Empty)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.IdRequired;
                return Ok(_response);
            }
            var request= new GetCustomerById();
            request.id = customerId;
            return  await _mediator.Send(request);

        }
        [Authorize(Roles ="Administrator,User")] // only accesed by user or Administrator
        [HttpPost]
        [Route("AddEditCustomer")]
        public async Task<object> AddEditCustomer(AddEditCommand model)
        {
            if(!ModelState.IsValid)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            //var request = new AddEditCommand();
            //request = model;
            return await _mediator.Send(model);
        }
    }
}
