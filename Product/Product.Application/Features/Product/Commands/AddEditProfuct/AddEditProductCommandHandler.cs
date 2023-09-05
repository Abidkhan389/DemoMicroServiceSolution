using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Contracts.Persistance;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Commands.AddEditProfuct
{
    internal class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommands, IResponse>
    {
        private readonly ILogger<AddEditProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;

        public AddEditProductCommandHandler(ILogger<AddEditProductCommandHandler> logger, IProductRepository productRepository)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));  
        }
        public async Task<IResponse> Handle(AddEditProductCommands request, CancellationToken cancellationToken)
        {
            var productobj = await _productRepository.AddEditProduct(request);
            if (productobj.Success == true)
            {
                if (request.ProductId != null)
                    _logger.LogInformation($"Customer {request.ProductId} is successfully Updated.");
                else
                    _logger.LogInformation($"Customer {request.ProductId} is successfully Created.");
            }
            return productobj;
        }
    }
}
