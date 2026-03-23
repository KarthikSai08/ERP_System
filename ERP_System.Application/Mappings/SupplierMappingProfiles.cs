using AutoMapper;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class SupplierMappingProfiles : Profile
    {
        public SupplierMappingProfiles()
        {
            CreateMap<Supplier, SupplierResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SupplierId))

                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SupplierName));
        }
    }
}
