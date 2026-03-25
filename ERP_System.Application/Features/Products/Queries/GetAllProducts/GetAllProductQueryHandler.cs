using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Queries.GetAllProducts;
using ERP_System.Application.Interfaces;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


public class GetAllProductQueryHandler
        : IRequestHandler<GetAllProductsQuery, ApiResponse<PagedResponse<ProductResponseDto>>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(
            IProductRepository prdRepo,
            IStockRepository stkRepo,
            IMapper mapper)
        {
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagedResponse<ProductResponseDto>>> Handle(
            GetAllProductsQuery query,
            CancellationToken ct)
        {

        //var version = await _cache.GetAsync<int>("products:version", ct);
        //if (version == 0) version = 1;

        //var cacheKey = $"products:v{version}:page:{query.PageNumber}:size:{query.PageSize}";

        var baseQuery = _prdRepo.GetQueryable()
                .Where(p => p.IsActive);

            var totalCount = await baseQuery.CountAsync(ct);

            var products = await baseQuery
                .OrderBy(p => p.ProductId)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(ct);

            var response = _mapper.Map<List<ProductResponseDto>>(products);

            
            var productIds = products.Select(p => p.ProductId).ToList();

            var stockDict = await _stkRepo.GetStockByProductIdsAsync(productIds, ct);

            foreach (var item in response)
            {
                if (stockDict.TryGetValue(item.Id, out var stock))
                    item.TotalStock = stock;
            }

            var paged = new PagedResponse<ProductResponseDto>(
                response,
                totalCount,
                query.PageNumber,
                query.PageSize
            );

            return ApiResponse<PagedResponse<ProductResponseDto>>.Ok(paged);
        }
    }
