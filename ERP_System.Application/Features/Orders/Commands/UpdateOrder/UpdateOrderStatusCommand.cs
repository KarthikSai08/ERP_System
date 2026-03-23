using ERP_System.Application.Common;
using ERP_System.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Commands.UpdateOrder
{
    public record UpdateOrderStatusCommand(int Id, OrderStatus newStatus) : IRequest<ApiResponse<bool>>
    {
    }
}
