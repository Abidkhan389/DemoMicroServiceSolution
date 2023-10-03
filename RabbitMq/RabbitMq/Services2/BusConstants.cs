using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Services2
{
    public class BusConstants
    {
        public const string? RabbitMqUri = "rabbitmq://localhost/";
        public const string? UserName = "guest";
        public const string? Password = "guest";
        public const string? OrderQueue = "validate-order-queue";
    }
}
