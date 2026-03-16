using AutoMapper;
using ERP_System.Application.Common.Extensions;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "N/A"))

                .ForMember(dest => dest.MarginPercent,
                    opt => opt.MapFrom(src => src.CalculateMargin()))
                
                .ForMember(dest => dest.TotalStock,
                    opt => opt.Ignore());
        }
    }
}
