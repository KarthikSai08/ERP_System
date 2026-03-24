using AutoMapper;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class OrderMappingProfiles : Profile
    {
        public OrderMappingProfiles()
        {
            CreateMap<Order, OrderResponseDto>()
             .ForMember(dest => dest.Id,
                 opt => opt.MapFrom(src => src.OrderId))

             .ForMember(dest => dest.CustomerName,
                 opt => opt.MapFrom(src => src.Customer != null
                     ? src.Customer.CustomerName
                     : string.Empty))

             .ForMember(dest => dest.Status,
                 opt => opt.MapFrom(src => src.Status.ToString()))

             .ForMember(dest => dest.Items,
                 opt => opt.MapFrom(src => src.Items));



            // OrderItem → OrderItemResponse
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.OrderItemId)) // adjust if different

                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product != null
                        ? src.Product.ProductName
                        : string.Empty))

                .ForMember(dest => dest.SKU,
                    opt => opt.MapFrom(src => src.Product != null
                        ? src.Product.SKU
                        : string.Empty))

                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        }
    }
    }
