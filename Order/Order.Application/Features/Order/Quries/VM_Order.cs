using Order.Domain.CustomerInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Order.Quries
{
    public class VM_Order
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ProductPrice { get; set; }
        public CustomerInfo customerInfo { get; set; }
    }
}
