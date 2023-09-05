using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Services.Services
{
    public class RequestMessage<TRequest>
    {
        public TRequest Payload { get; set; }
    }
}
