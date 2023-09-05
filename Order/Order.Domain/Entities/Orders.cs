using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class Orders
    {
        [Key]
        public string OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderedOn { get; set; }
        public List<OrdersDetails> OrdersDetails { get; set; }
    }
}
