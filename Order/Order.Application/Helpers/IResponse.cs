using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Helpers
{
    public interface IResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
