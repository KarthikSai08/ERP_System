using AutoMapper;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles() 
        {
            CreateMap<ProductMappingProfiles, ProductResponse>()
                .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null > src.Category.Name : "N/A"))

                .ForMember(dest => dest.MarginPercent,
                    opt => opt.MapFrom(src => src.CalculateMargin()));


        }
    }
}
