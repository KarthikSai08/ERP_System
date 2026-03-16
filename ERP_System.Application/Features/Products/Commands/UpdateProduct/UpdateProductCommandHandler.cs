using ERP_System.Application.Common;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        public UpdateProductCommandHandler(IProductRepository prdRepo, IStockRepository stkRepo)
        {
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(UpdateProductCommand cmd, CancellationToken ct)
        {
            var prd = await _prdRepo.GetByIdAsync(cmd.ProductId,ct)
                ?? throw new NotFoundException("Product", cmd.ProductId);

            prd.Update(cmd.PrdName, cmd.Description, cmd.Price, cmd.CostPrice);
            await _prdRepo.UpdateAsync(prd,ct);

            var totalStock = await _stkRepo.GetTotalStockAsync(cmd.ProductId,ct);

            return ApiResponse<ProductResponse>.Ok(
                new ProductResponse(prd.ProductId, prd.ProductName,
                                    prd.SKU, prd.Description,
                                    prd.Price, prd.CostPrice,
                prd.Price > 0 ? Math.Round(((prd.Price - prd.CostPrice)/prd.Price)*100, 2): 0,
                prd.Category?.CategoryName ?? "N/A", 
                prd.IsActive, totalStock),
                "Prodct Updated Successfully");
        }
    }
}
