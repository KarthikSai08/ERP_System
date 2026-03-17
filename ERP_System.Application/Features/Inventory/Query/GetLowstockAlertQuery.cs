using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Inventory.Query
{
    public record GetLowstockAlertQuery : IRequest<ApiResponse<IEnumerable<LowStockAlertResponse>>>;
   
}
