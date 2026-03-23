using AutoMapper;
using ERP_System.Domain.Entities;
using ERP_System.Application.DTOs;

namespace ERP_System.Application.Common.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
