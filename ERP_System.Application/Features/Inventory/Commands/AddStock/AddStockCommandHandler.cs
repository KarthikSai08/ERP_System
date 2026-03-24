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

namespace ERP_System.Application.Features.Inventory.Command.AddStock
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, ApiResponse<StockResponseDto>>
    {
        private readonly IStockRepository _stckRepo;
        private readonly IProductRepository _prdRepo;
        private readonly IMapper _mapper;

        public AddStockCommandHandler(IStockRepository stckRepo, IProductRepository prdRepo, IMapper mapper)
        {
            _stckRepo = stckRepo;
            _prdRepo = prdRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<StockResponseDto>> Handle(AddStockCommand cmd, CancellationToken ct)
        {
            var prd = await _prdRepo.GetByIdAsync(cmd.productId,ct)
                 ?? throw new NotFoundException("Product" , cmd.productId);

            var stock = await _stckRepo.GetByProductAndWarehouseAsync(cmd.productId,cmd.warehouseId, ct);

            if(stock is null)
            {
                stock = Stock.Create(cmd.productId, cmd.warehouseId, cmd.Quantity, cmd.ReorderLevel);

                await _stckRepo.AddAsync(stock,ct);
            }
            else
            {
                stock.AddStock(cmd.Quantity);
                await _stckRepo.UpdateAsync(stock,ct);
            }

            var res = _mapper.Map<StockResponseDto>(stock);

            res.ProductName = prd.ProductName;
            res.SKU = prd.SKU;

            return ApiResponse<StockResponseDto>.Ok(res, "Stock Added Successfully");
        }
    }
}
