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
        public Orders(DateTime OrderedOn, List<OrdersDetails> ordersDetails, Guid customerid)
        {
            this.CustomerId =Guid.NewGuid();
            this.OrderedOn=OrderedOn;
            this.OrdersDetails = ordersDetails;
            this.OrderId= GenerateUniqueOrderId();
        }
        private string GenerateUniqueOrderId()
        {
            // Generate a unique string for OrderId using a combination of timestamp and a random number
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff"); // Use a custom format for timestamp
            string randomNumber = new Random().Next(10000, 99999).ToString(); // Generate a random 5-digit number

            return timestamp + randomNumber;
        }
    }
}
