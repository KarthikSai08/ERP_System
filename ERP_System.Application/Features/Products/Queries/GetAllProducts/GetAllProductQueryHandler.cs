using ERP_System.Application.Common;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<IEnumerable<ProductResponse>>>
    {
        private readonly IProductRepository _PrdRepo;
        private readonly IStockRepository _StkRepo;
        public GetAllProductQueryHandler(IProductRepository prdRepo, IStockRepository stkRepo)
        {
            _PrdRepo = prdRepo;
            _StkRepo = stkRepo;
        }

        public async Task<ApiResponse<IEnumerable<ProductResponse>>> 
            Handle(GetAllProductsQuery query,CancellationToken ct) 
        {
            var prd = await _PrdRepo.GetAllAsync(ct);
            var ids = prd.Select(p => p.ProductId).ToList();

            var stockMap = await _StkRepo.GetTotalStockBatchAsync(ids, ct);

            var result = prd.Select(p => new ProductResponse (
                p.ProductId, p.ProductName, p.SKU, p.Description, p.Price, p.CostPrice,
                p.Price > 0 ? Math.Round(((p.Price - p.CostPrice) / p.Price) * 100, 2) : 0,
                p.Category?.CategoryName ?? "N/A",
                p.IsActive,
                stockMap.GetValueOrDefault(p.ProductId, 0)
             )).ToList();

            return ApiResponse<IEnumerable<ProductResponse>>.Ok(result);        }
        
    }
}
