using AutoMapper;
using CustomerWebApi.Features.Customers.Commands.AddEdit;
using CustomerWebApi.Models;
using MassTransit;
using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Order.Application.Contracts.Persistance;
using Order.Application.Features.Order.Commands.AddEditOrder;
using Order.Application.Features.Order.Quries;
using Order.Application.Helpers;
using Order.Domain.CustomerInfo;
using Order.Domain.Entities;
using Order.Infrastructure.Persistance;
using RabbitMq.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;
        private readonly IResponse _response;
        private readonly IRequestClient<Customer> _customerDetailsRequestClient;

        public OrderRepository(OrderDbContext context, IMapper mapper, IResponse response, IRequestClient<Customer> _customerDetailsRequestClient)
        {

            this._context = context;
            this._mapper = mapper;
            this._response = response;
            this._customerDetailsRequestClient = _customerDetailsRequestClient;
        }

        public async Task<IResponse> AddEditOrder(AddEditOrderCommands model)
        {
            if (model.OrderId == null)
            {
                var Customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    Email = model.CustomerEmail,
                    MobileNumber = model.CustomerPhone
                };
                var customerupdate = _customerDetailsRequestClient.GetResponse<Customer>(Customer);
                var order = _mapper.Map<Orders>(model);
                    var orderdetails = _mapper.Map<OrdersDetails>(model.OrdersDetails);
                    await _context.Orders.AddAsync(order);
                    await _context.OrdersDetails.AddAsync(orderdetails);
                    await _context.SaveChangesAsync();
                    _response.Data = order;
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataSaved;
                    return _response;
               
            }
            else
            {
                var orderObj = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == model.OrderId);
                if (orderObj != null)
                {
                    var Customer = new Customer
                    {
                        CustomerName=model.CustomerName,
                        CustomerId=orderObj.CustomerId,
                        Email=model.CustomerEmail,
                        MobileNumber=model.CustomerPhone
                    };
                    var customerupdate = _customerDetailsRequestClient.GetResponse<Customer>(Customer);
                    // Customer customerobj= _mapper.Map<Customer>(model);
                    _context.Orders.Update(_mapper.Map<Orders>(model));
                    _context.OrdersDetails.Update(_mapper.Map<OrdersDetails>(model.OrdersDetails));
                    await _context.SaveChangesAsync();
                    _response.Success = Constants.ResponseSuccess;
                    _response.Message = Constants.DataUpdate;
                    return _response;
                }
                _response.Message = Constants.NotFound.Replace("{data}", "Order");
                _response.Success = Constants.ResponseFailure;
                return _response;

            }
        }

        public async Task<IResponse> DeleteOrder(Guid id)
        {
            var OrderToDelete = await GetOrderById(id);

            if (OrderToDelete.Success && OrderToDelete.Data is Orders orders)
            {
                _context.Orders.Remove(orders);
                await _context.SaveChangesAsync();

                _response.Success = Constants.ResponseSuccess;
                _response.Message = Constants.DataDelete;
            }
            else
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = "Order not found.";
            }

            return _response;
        }

        public async Task<IResponse> GetAllOrders()
        {
            var productlist = await _context.Orders.ToListAsync();
            _response.Data = productlist;
            _response.Success = Constants.ResponseSuccess;
            _response.Message = "All Orders List";
            return _response;
        }

        public async Task<IResponse> GetOrderById(Guid id)
        {
            var orderObj = await _context.Orders.FindAsync(id);
            if (orderObj != null)
            {
               // Use RabbitMQ to request customer information from the Customer microservice
                var customerInfo = await _customerDetailsRequestClient.GetResponse<Customer>(
                new Customer { CustomerId = orderObj.CustomerId });

                var OrdercustomerViewModel = _mapper.Map<VM_Order>(orderObj);
                OrdercustomerViewModel.customerInfo= (CustomerInfo)customerInfo;

                _response.Success = Constants.ResponseSuccess;
                _response.Message = "Order found.";
                _response.Data = OrdercustomerViewModel;
                return _response;
            }
            else
            {
                _response.Success = Constants.ResponseFailure;
                _response.Message = "Order not found.";
                return _response;
            }
        }
        //private async Task<CustomerInfo> RequestCustomerInfoFromCustomerMicroservice(Guid customerId)
        //{
        //    // Create a message to request customer information
        //    var request = new CustomerInfoRequest
        //    {
        //        CustomerId = customerId
        //    };

        //    // Serialize the request message
        //    var messageBody = JsonConvert.SerializeObject(request);

        //    // Publish the request to RabbitMQ
        //    _rabbitMqService.PublishMessage(messageBody, "customer_info_requests");

        //    // Wait for the response from RabbitMQ
        //    //var response =  _rabbitMqService.WaitForResponse<CustomerInfoResponse>("customer_info_responses");
        //    return response.CustomerInfo;
        //}
    }
}
