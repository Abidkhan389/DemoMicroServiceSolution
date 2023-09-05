using Product.Application.Features.Product.Commands.AddEditProfuct;
using Product.Application.Features.Product.Quries;
using Product.Application.Helpers;
using Product.Application.Helpers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Persistance
{
    public interface IProductRepository
    {
        Task<IResponse> GetAllProducts(GetProductList model);
        Task<IResponse> GetProductById(Guid id);
        Task<IResponse> AddEditProduct(AddEditProductCommands model);
        Task<IResponse> DeleteProduct(Guid id);

    }
}
