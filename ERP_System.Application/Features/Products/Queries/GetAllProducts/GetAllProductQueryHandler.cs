using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<IEnumerable<ProductResponseDto>>>
    {
        private readonly IProductRepository _PrdRepo;
        private readonly IStockRepository _StkRepo;
        private readonly IMapper _mapper;
        public GetAllProductQueryHandler(IProductRepository prdRepo, IStockRepository stkRepo,IMapper mapper)
        {
            _PrdRepo = prdRepo;
            _StkRepo = stkRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ProductResponseDto>>> 
            Handle(GetAllProductsQuery query,CancellationToken ct) 
        {
            var prd = await _PrdRepo.GetAllAsync(ct);
            
            var response = _mapper.Map<List<ProductResponseDto>>(prd);

            var stockTasks = response.Zip(prd, (r, p) => new { Respone = r, ProductId = p.ProductId })
                .Select(async x =>
                {
                    x.Respone.TotalStock = await _StkRepo.GetTotalStockAsync(x.ProductId, ct);
                }
                );
            await Task.WhenAll(stockTasks);

            return ApiResponse<IEnumerable<ProductResponseDto>>.Ok(response);        }
        
    }
}
