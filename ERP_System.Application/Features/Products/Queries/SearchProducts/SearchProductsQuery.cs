using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.SearchProducts
{
    public  record SearchProductsQuery(string? name, int? CategoryId, decimal? maxPrice) : IRequest<ApiResponse<IEnumerable<ProductResponseDto>>>    {
    }
}
