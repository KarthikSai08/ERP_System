using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Employees.Queries.GetAllEmployees
{
    public record GetAllEmployeesQuery : IRequest<ApiResponse<IEnumerable<EmployeeResponseDto>>>;
}
