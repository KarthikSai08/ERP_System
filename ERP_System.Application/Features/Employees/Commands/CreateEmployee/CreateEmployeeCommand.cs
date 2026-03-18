using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Employees.Commands.CreateEmployee
{
    public record CreateEmployeeCommand(string firstName, string lastName, string email,
            string phone, string department, string designation, decimal salary) : IRequest<ApiResponse<EmployeeResponseDto>>;
}
