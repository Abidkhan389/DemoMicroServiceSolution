
using Order.Application.Features.Order.Commands.AddEditOrder;
using Order.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Contracts.Persistance
{
    public interface IOrderRepository
    {
        Task<IResponse> GetAllOrders();
        Task<IResponse> GetOrderById(Guid id);
        Task<IResponse> AddEditOrder(AddEditOrderCommands model);
        Task<IResponse> DeleteOrder(Guid id);

    }
}
