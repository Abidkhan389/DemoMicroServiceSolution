using AutoMapper;
using Product.Application.Features.Product.Commands.AddEditProfuct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product.domain.Entities.Product, AddEditProductCommands>().ReverseMap();
        }
    }
}
