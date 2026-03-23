using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ERP_System.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ApiResponse<IEnumerable<EmployeeResponseDto>>>
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository empRepo, IMapper mapper)
        {
            _mapper = mapper;
            _empRepo = empRepo;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeResponseDto>>> Handle(GetAllEmployeesQuery qry, CancellationToken ct)
        {
            var employees = await _empRepo.GetAllAsync(ct);

            var res =_mapper.Map<IEnumerable<EmployeeResponseDto>>(employees);

            return ApiResponse<IEnumerable<EmployeeResponseDto>>.Ok(res);
        }
    }
}
