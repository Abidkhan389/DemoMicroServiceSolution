using AutoMapper;
using CustomerWebApi.Contracts.Persistence.GeneralServices;
using CustomerWebApi.DTO;
using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Features.Customers.Quries;
using CustomerWebApi.Helpers;
using CustomerWebApi.Models;
using CustomerWebApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerWebApi.Contracts.Persistence.ICustomer
{
    public interface ICustomerRepository
    {
        Task<IResponse> GetAllCustomer(GetCustomersList model);
        Task<IResponse> GetCustomerById(Guid Id);
        Task<IResponse> AddEditCustomer(AddEditCommand model);
        Task<IResponse> DeleteCustomer(Customer model);
    }
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        private readonly ICountResponse _countResp;

        public CustomerRepository(CustomerDbContext context, IMapper mapper, IResponse response, ICountResponse countResp)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = response;
            this._countResp = countResp;
            _countResp = countResp;
        }

        public async Task<IResponse> GetAllCustomer(GetCustomersList model)
        {
            var Customerlist = _context.Customer
                                .Select(x => new VW_Customer
                                {
                                    CustomerId = x.CustomerId,
                                    CustomerName = x.CustomerName,
                                    MobileNumber = x.MobileNumber,
                                    Email = x.Email,
                                }).AsQueryable();
            var count = Customerlist.Count();
            var sorted = await HelperStatic.OrderBy(Customerlist, model.SortEx, model.OrderEx == "desc").Skip(model.Start).Take(model.LimitEx).ToListAsync();
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

        public async Task<IResponse> GetCustomerById(Guid Id)
        {
            Customer customer = await _context.Customer.FindAsync(Id);
            if (customer != null)
            {
                //var customerViewModel = _mapper.Map<VM_Customer>(customer);
                _response.Success = Constants.ResponseSuccess;
                _response.Message = "Customer found.";
                _response.Data = customer;
                return _response;
            }
            else
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = "Customer not found.";
                return _response;
            }

        }
        public async Task<IResponse> AddEditCustomer(AddEditCommand model)
        {
            if (model.CustomerId != null)
            {
                var customerObj = await _context.Customer.FirstOrDefaultAsync(x => x.CustomerId == model.CustomerId);
                if (customerObj != null)
                {
                    //Customer customerobj= _mapper.Map<Customer>(model);
                    _context.Customer.Update(_mapper.Map<Customer>(model));
                    await _context.SaveChangesAsync();
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataUpdate;
                    return _response;
                }
                _response.Message = Constants.NotFound.Replace("{data}", "Course");
                _response.Success = Constants.ResponseFailure;
                return _response;
            }
            else
            {
                var customerExist = await _context.Customer.FirstOrDefaultAsync(x => x.CustomerName == model.CustomerName
                                        && x.MobileNumber == model.MobileNumber);
                if (customerExist == null)
                {
                    Customer customer = new Customer(model);
                    await _context.Customer.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    _response.Data = customer;
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataSaved;
                    return _response;
                }
                _response.Message = Constants.Exists.Replace("{data}", "Customer");
                _response.Success = Constants.ResponseFailure;
                return _response;
            }
        }

        public async Task<IResponse> DeleteCustomer(Customer model)
        {
            _context.Customer.Remove(model);
            await _context.SaveChangesAsync();

            _response.Success = Constants.ResponseSuccess;
            _response.Message = Constants.DataDelete;
            return _response;
        }
    }
}
