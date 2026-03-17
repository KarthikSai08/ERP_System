using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, ApiResponse<IEnumerable<ProductResponseDto>>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IMapper _mapper;
        private readonly IStockRepository _stkRepo;
        public SearchProductsQueryHandler(IProductRepository prdRepo, IMapper mapper, IStockRepository stkRepo)
        {
            _prdRepo = prdRepo;
            _mapper = mapper;
            _stkRepo = stkRepo;
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> Handle(SearchProductsQuery qry, CancellationToken ct)
        {
            var products = await _prdRepo.SearchAsync(qry.name, qry.CategoryId, qry.maxPrice, ct);

            var res = _mapper.Map<List<ProductResponseDto>>(products);

            var stockTasks = res.Zip(products, (r, p) => new
            {
                Response = r,
                ProductId = p.ProductId
            })
                .Select(async x =>
                {
                    x.Response.TotalStock = await _stkRepo.GetTotalStockAsync(x.ProductId, ct);
                });

            await Task.WhenAll(stockTasks);

            return ApiResponse<IEnumerable<ProductResponseDto>>.Ok(res);

        }
    }
}
