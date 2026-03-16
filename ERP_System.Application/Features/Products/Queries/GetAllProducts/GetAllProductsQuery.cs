using ERP_System.Application.Common;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<ApiResponse<IEnumerable<ProductResponse>>>
    {
    }
}
