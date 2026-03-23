using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Warehouse.Commands
{
    public record CreateWarehouseCommand(string WarehouseName, string Location) : IRequest<ApiResponse<WarehouseResponseDto>>
    {
    }
}
