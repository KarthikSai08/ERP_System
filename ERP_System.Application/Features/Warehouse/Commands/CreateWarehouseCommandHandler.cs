using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Warehouse.Commands
{
    public class  CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, ApiResponse<WarehouseResponseDto>>
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
            
        }
    }
}
