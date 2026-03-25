using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Customers.Queries.GetAllCustomers
{
    public record  GetAllCustomersQuery : PaginationRequest, ICacheableQuery, IRequest<ApiResponse<PagedResponse<CustomerResponseDto>>>
    {
        public int Version { get; init; } = 1;
        public string CacheKey => $"customers:v{Version}:page:{PageNumber}:size:{PageSize}";
        public TimeSpan CacheExpiration => TimeSpan.FromMinutes(15);
    }
}
