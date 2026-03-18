using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeResponseDto>>
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository empRepo, IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeResponseDto>> Handle(CreateEmployeeCommand cmd, CancellationToken ct)
        {
            if (await _empRepo.EmailExistsAsync(cmd.email,ct))
                throw new ConflictException("Employee with email @email Already Exists");

            var emp = Employee.Create(cmd.firstName, cmd.lastName, cmd.email, cmd.phone, cmd.department, cmd.designation, cmd.salary);
                await _empRepo.AddAsync(emp, ct);

            var res = _mapper.Map<EmployeeResponseDto>(emp);
                return ApiResponse<EmployeeResponseDto>.Ok(res);
        }
    }
}
