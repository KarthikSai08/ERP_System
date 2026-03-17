using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Inventory.Command.AddStock
{
    public record AddStockCommand(int productId, int warehouseId, int Quantity, int ReorderLevel = 10) : IRequest<ApiResponse<StockResponseDto>>;
}
