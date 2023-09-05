using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts.Persistance;
using Product.Application.Features.Product.Commands.AddEditProfuct;
using Product.Application.Features.Product.Quries;
using Product.Application.Helpers;
using Product.Application.Helpers.General;
using Product.Infrastructure.DTO;
using Product.Infrastructure.Persistance;
using Product.Infrastructure.Repositories.GeneralServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        private readonly ICountResponse _countResp;

        public ProductRepository(ProductDbContext context, IMapper mapper, IResponse response, ICountResponse countResp)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = response;
            this._countResp = countResp;
            _countResp = countResp;
        }

        public async Task<IResponse> GetAllProducts (GetProductList model)
        {
            //var data = await _dbContext.Province.Where(x => x.Status == 1).Select(x => new { Id = x.ProvinceId, Text = x.ProvinceName }).OrderBy(x => x.Text).ToListAsync();
            //bool? bStatus = model?.ActiveStatus == 1 ? true : (model?.ActiveStatus == 0 ? false : (bool?)null);
            var productlist =  _context.Product
                            .Where(y => y.Status == model.ActiveStatus || model.ActiveStatus == null)
                                .Select(x=> new VW_Product
                                {
                                    ProductCode= x.ProductCode,
                                    ProductName= x.ProductName,
                                    ProductId= x.ProductId,
                                    ProductPrice= x.ProductPrice,
                                    Status=x.Status,
                                }).AsQueryable();
            var count = productlist.Count();
            var sorted = await HelperStatic.OrderBy(productlist, model.SortEx, model.OrderEx == "desc").Skip(model.Start).Take(model.LimitEx).ToListAsync();
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

        public async Task<IResponse> AddEditProduct(AddEditProductCommands model)
        {
            if(model.ProductId == null)
            {
                var productobj= await _context.Product.FirstOrDefaultAsync(x=> x.ProductName==model.ProductName);
                if(productobj == null)
                {
                    var product = _mapper.Map<Product.domain.Entities.Product>(model);
                    await _context.Product.AddAsync(product);
                    await _context.SaveChangesAsync();
                    _response.Data = product;
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataSaved;
                    return _response;
                }
                _response.Message = Constants.Exists.Replace("{data}", "Product");
                _response.Success = Constants.ResponseFailure;
                return _response;
            }
            else
            {
                var productObj = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
                if (productObj != null)
                {
                    // Customer customerobj= _mapper.Map<Customer>(model);
                    _context.Product.Update(_mapper.Map<Product.domain.Entities.Product>(model));
                    await _context.SaveChangesAsync();
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataUpdate;
                    return _response;
                }
                _response.Message = Constants.NotFound.Replace("{data}", "Course");
                _response.Success = Constants.ResponseFailure;
                return _response;

            }
            
        }

        public async Task<IResponse> DeleteProduct(Guid id)
        {
            var productToDelete = await GetProductById(id);

            if (productToDelete.Success && productToDelete.Data is Product.domain.Entities.Product product)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                _response.Success = Constants.ResponseSuccess;
                _response.Message = Constants.DataDelete;
            }
            else
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = "Product not found.";
            }

            return _response;
        }


        public async Task<IResponse> GetProductById(Guid id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                //var customerViewModel = _mapper.Map<VM_Customer>(customer);
                _response.Success = Constants.ResponseSuccess;
                _response.Message = "Customer found.";
                _response.Data = product;
                return _response;
            }
            else
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = "Customer not found.";
                return _response;
            }
        }
    }
}
