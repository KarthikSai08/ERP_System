using ERP_System.Application.Common;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Queries.GetSalesReport
{
    public class GetSalesReportQueryHandler : IRequestHandler<GetSalesReportQuery, ApiResponse<IEnumerable<SalesReportResponseDto>>>
    {
        private readonly IOrderRepository  _repo;
    public GetSalesReportQueryHandler(IOrderRepository repo) =>  _repo = repo;

    public async Task<ApiResponse<IEnumerable<SalesReportResponseDto>>> Handle(
        GetSalesReportQuery query, CancellationToken cancellationToken)
        {
            var report = await  _repo.GetSalesReportAsync(query.From, query.To);
            return ApiResponse<IEnumerable<SalesReportResponseDto>>.Ok(report);
        }
    }
}
