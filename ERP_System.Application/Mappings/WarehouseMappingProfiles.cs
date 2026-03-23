using AutoMapper;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class WarehouseMappingProfilesc : Profile
    {
        public WarehouseMappingProfilesc()
        {
            CreateMap<Warehouse, WarehouseResponseDto>();
        }
    }
}
