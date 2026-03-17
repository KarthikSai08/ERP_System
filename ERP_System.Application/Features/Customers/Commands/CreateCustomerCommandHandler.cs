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

namespace ERP_System.Application.Features.Customers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponseDto>>
    {
        private readonly ICustomerRepository _cstRepo;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository cstRepo, IMapper mapper)
        {
            _cstRepo = cstRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CustomerResponseDto>> Handle(CreateCustomerCommand cmd, CancellationToken ct)
        {
            var cstMail = await _cstRepo.EmailExistsAsync(cmd.email, ct);
            if (cstMail != null)
                throw new ConflictException($"Customer with Email {cmd.email} already Exists");

            var customer = Customer.Create(cmd.name, cmd.email, cmd.phone, cmd.Address);

            await _cstRepo.AddAsync(customer,ct);

            var res = _mapper.Map<CustomerResponseDto>(customer);

            return ApiResponse<CustomerResponseDto>.Ok(res);
        }
    }
}
