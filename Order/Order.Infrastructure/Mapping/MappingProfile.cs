using AutoMapper;
using Order.Application.Features.Order.Commands.AddEditOrder;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Orders, AddEditOrderCommands>().ReverseMap();
            CreateMap<OrdersDetails, AddEditOrderCommands>().ReverseMap();
        }
    }
}
