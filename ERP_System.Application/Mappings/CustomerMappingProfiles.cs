using AutoMapper;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Customers.Commands;
using ERP_System.Domain.Entities;

namespace ERP_System.Application.Common.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Customer → CustomerResponseDto
            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // CreateCustomerCommand → Customer
            CreateMap<CreateCustomerCommand, Customer>()
                .ConstructUsing(src => Customer.Create(src.name, src.email, src.phone, src.Address));
        }
    }
}