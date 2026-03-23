using AutoMapper;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Employees.Commands.CreateEmployee;
using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Mappings
{
    public class EmployeesMappingProfiles : Profile
    {
        public EmployeesMappingProfiles()
        {
            CreateMap<Employee, EmployeeResponseDto>();

        }
    }
}
