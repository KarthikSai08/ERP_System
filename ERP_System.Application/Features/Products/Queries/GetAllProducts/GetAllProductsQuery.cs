using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : PaginationRequest, ICacheableQuery, IRequest<ApiResponse<PagedResponse<ProductResponseDto>>>
    {
        public int Version { get; init; } = 1;
        public string CacheKey => $"products:v{Version}:page:{PageNumber}:size:{PageSize}";
        public TimeSpan CacheExpiration => TimeSpan.FromMinutes(15);
    }
}
