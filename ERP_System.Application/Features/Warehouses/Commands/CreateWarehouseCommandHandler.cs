using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using ERP_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Warehouses.Commands
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, ApiResponse<WarehouseResponseDto>>
    {
        private readonly IWarehouseRepository _warehouseRepo;
        private readonly IMapper _mapper;

        public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepo, IMapper mapper)
        {
            _warehouseRepo = warehouseRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<WarehouseResponseDto>> Handle(CreateWarehouseCommand cmd, CancellationToken ct)
        {
            var existWarehouse = await _warehouseRepo.WarehouseExistsAsync(cmd.WarehouseName,ct);
            if(existWarehouse == true)
                throw new ConflictException($"Warehouse {cmd.WarehouseName} already exists.");


            var warehouse = Warehouse.Create(cmd.WarehouseName, cmd.Location);
            await _warehouseRepo.AddAsync(warehouse,ct);
            var result =  _mapper.Map<WarehouseResponseDto>(warehouse);

            return ApiResponse<WarehouseResponseDto>.Ok(result,"Warehouse Created Sucessfully");
        }
    }
}
