using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Order.Commands.AddEditOrder;
using Order.Application.Features.Order.Quries;
using Order.Application.Features.Product.Commands.DeleteProduct;
using Product.Application.Features.Product.Commands.AddEditProfuct;
using Product.Application.Features.Product.Commands.DeleteProduct;
using Product.Application.Features.Product.Quries;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<object> GetAllOrders()
        {
            var query = new GetOrderList();
            return await _mediator.Send(query);
        }
        [HttpGet]
        [Route("GetOrderById/{OrderId}")]
        public async Task<object> GetOrderById(Guid OrderId)
        {
            var request = new GetOrderById(OrderId);
            return await _mediator.Send(request);
        }
        [HttpPost]
        [Route("AddEditOrder")]
        public async Task<object> AddEditOrder(AddEditOrderCommands model)
        {
            //var request = new AddEditCommand();
            //request = model;
            return await _mediator.Send(model);
        }
        [HttpDelete]
        [Route("DeleteOrder/{OrderId")]
        public async Task<object> DeleteProduct(Guid OrderId)
        {
            var request = new DeleteOrderCommand(OrderId);
            return await _mediator.Send(request);
        }
    }
}
