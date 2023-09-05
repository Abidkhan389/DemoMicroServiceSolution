using MediatR;
using Product.Application.Contracts.Persistance;
using Product.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Features.Product.Quries
{
    internal class GetProductByIdHandlerQuery : IRequestHandler<GetProductById, IResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandlerQuery(IProductRepository productRepository)
        {
            this._productRepository = productRepository?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<IResponse> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var customer = await _productRepository.GetProductById(request.id);
            return customer;
        }
    }
}
