using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(int CustomerId, string? Notes, List<OrderItemRequest> Items) : IRequest<ApiResponse<OrderResponseDto>>;

    public record OrderItemRequest(int ProductId, int Quantity);
}
