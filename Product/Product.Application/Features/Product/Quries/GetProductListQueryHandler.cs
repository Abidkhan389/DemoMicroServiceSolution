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
    public class GetProductListQueryHandler : IRequestHandler<GetProductList, IResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductListQueryHandler(IProductRepository productRepository)
        {
            this._productRepository = productRepository?? throw new ArgumentNullException(nameof(productRepository));
        }
        public async Task<IResponse> Handle(GetProductList request, CancellationToken cancellationToken)
        {
            var productlist = await _productRepository.GetAllProducts(request);
            return productlist;
        }
    }
}
