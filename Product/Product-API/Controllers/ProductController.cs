using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Product.Commands.AddEditProfuct;
using Product.Application.Features.Product.Commands.DeleteProduct;
using Product.Application.Features.Product.Quries;
using Product.Application.Helpers;
using Product.domain.Entities;

namespace Product_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IResponse _response;

        public ProductController(IMediator mediator, IResponse response)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._response = response;
        }
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<object> GetAllProducts([FromBody] GetProductList model )
        {
            if (!ModelState.IsValid)
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = Constants.ModelStateStateIsInvalid;
                return Ok(_response);
            }
            //var query = new GetProductList();
            return await _mediator.Send(model);
        }
        [HttpGet]
        [Route("GetProductById/{ProductId}")]
        public async Task<object> GetProductById(Guid ProductId)
        {
            var request = new GetProductById(ProductId);
            return await _mediator.Send(request);
        }
        [HttpPost]
        [Route("AddEditProduct")]
        public async Task<object> AddEditProduct(AddEditProductCommands model)
        {
            //var request = new AddEditCommand();
            //request = model;
            return await _mediator.Send(model);
        }
        [HttpDelete]
        [Route("DeleteProduct/{ProductId")]
        public async Task<object> DeleteProduct(Guid PorductId)
        {
            var request = new DeleteProductCommand(PorductId);
            return await _mediator.Send(request);
        }
    }
}
