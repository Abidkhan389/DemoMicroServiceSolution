using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistance;
using Product.Application.Features.Product.Commands.AddEditProfuct;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ILogger<DeleteProductCommandHandler> logger)
        {
            this._productRepository = _productRepository ?? throw new ArgumentNullException(nameof(_productRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productobj = await _productRepository.DeleteProduct(request.ProductiId);
            if(productobj.Success == true)
            {
                    _logger.LogInformation($"Product is successfully deleted.");
            }
            return productobj;
        }
    }
}
