using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.CustomerInfo
{
    public class CustomerInfo
    {
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
