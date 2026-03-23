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

namespace ERP_System.Application.Features.Suppliers.Commands.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, ApiResponse<SupplierResponseDto>>
    {
        private readonly ISupplierRepository _supRepo;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(ISupplierRepository supRepo, IMapper mapper)
        {
            _mapper = mapper;
            _supRepo = supRepo;
        }
        public async Task<ApiResponse<SupplierResponseDto>> Handle(CreateSupplierCommand cmd, CancellationToken ct)
        {
            if (await _supRepo.EmailExistsAsync(cmd.email,ct))
                throw new ConflictException($"Supplier with email {cmd.email} already exists");

            var supplier = Supplier.Create(cmd.name,cmd.contactPerson,cmd.email,cmd.phone,cmd.address);
            await _supRepo.AddAsync(supplier, ct);

            var res = _mapper.Map<SupplierResponseDto>(supplier);
            return ApiResponse<SupplierResponseDto>.Ok(res);
        }
    }
}
