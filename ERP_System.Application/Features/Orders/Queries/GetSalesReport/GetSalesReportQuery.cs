using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Queries.GetSalesReport
{
    public record GetSalesReportQuery(DateTime From, DateTime To) : IRequest<ApiResponse<IEnumerable<SalesReportResponseDto>>>;
}
