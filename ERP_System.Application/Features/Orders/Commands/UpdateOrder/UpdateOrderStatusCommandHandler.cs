using ERP_System.Application.Common;
using ERP_System.Domain.Enums;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, ApiResponse<bool>>
    {
        private readonly IOrderRepository _orderRepo;
        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<ApiResponse<bool>> Handle(UpdateOrderStatusCommand cmd, CancellationToken ct)
        {
            var order = await _orderRepo.GetByIdAsync(cmd.Id,ct)
                ?? throw new NotFoundException("Order", cmd.Id);

            switch (cmd.newStatus)
            {
                case OrderStatus.Confirmed: order.Confirm(); break;
                case OrderStatus.Shipped: order.Ship(); break;
                case OrderStatus.Delivered: order.Deliver() ; break;
                case OrderStatus.Cancelled: order.Cancel(); break;
                default: throw new ValidationException("Invalid Status Transistion");
            }
            await _orderRepo.UpdateAsync(order, ct);
            return ApiResponse<bool>.Ok(true, $"Order status updated To {cmd.newStatus}");
        }
    }
}
