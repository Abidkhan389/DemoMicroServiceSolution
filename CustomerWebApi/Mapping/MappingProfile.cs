using AutoMapper;
using CustomerWebApi.Features.Customers.Quries;
using CustomerWebApi.Models;

namespace CustomerWebApi.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Customer,VM_Customer>().ReverseMap();
        }
    }
}
