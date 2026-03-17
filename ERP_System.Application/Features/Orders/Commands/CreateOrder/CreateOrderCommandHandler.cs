using AutoMapper;
using System.Linq;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _cstRepo;
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepo, ICustomerRepository cstRepo, IProductRepository prdRepo, IStockRepository stkRepo, IUnitOfWork uow, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _cstRepo = cstRepo;
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponseDto>> Handle(CreateOrderCommand cmd, CancellationToken ct)
        {
            var customer = await _cstRepo.GetByIdAsync(cmd.CustomerId,ct)
                ?? throw new NotFoundException("Customer", cmd.CustomerId);

            if (!customer.IsActive)
                throw new ValidationException("Cannot create order for inactive customer");

            await _uow.BeginTransactionAsync();
            try
            {
                var order = Order.Create(cmd.CustomerId, cmd.Notes);
                var orderId = await _orderRepo.AddAsync(order,ct);

                foreach (var itemReq in cmd.Items)
                {
                    var product = await _prdRepo.GetByIdAsync(itemReq.ProductId,ct)
                        ?? throw new NotFoundException("Product", itemReq.ProductId);

                    if (!product.IsActive)
                        throw new ValidationException($"Product '{product.ProductName}' is not active.");

                    var stock = await _stkRepo.GetByProductAndWarehouseAsync(itemReq.ProductId, 1,ct);
                    if (stock is null || stock.Quantity < itemReq.Quantity)
                        throw new InsufficientStockException(
                            product.ProductName, itemReq.Quantity, stock?.Quantity ?? 0);

                    var item = OrderItem.Create(orderId, itemReq.ProductId, itemReq.Quantity, product.Price);
                    await _orderRepo.AddItemAsync(item, ct);

                    stock.DeductStock(itemReq.Quantity);
                    await _stkRepo.UpdateAsync(stock, ct);

                    await _uow.CommitAsync();

                    var created = await _orderRepo.GetByIdWithItemsAsync(orderId, ct);
                    var res = _mapper.Map<OrderResponseDto>(created);

                    if (res.CustomerName == "N/A")
                        res.CustomerName = customer.CustomerName;

                    return ApiResponse<OrderResponseDto>.Ok(res, "Order created Successfully");
                }

            }
            catch 
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}
