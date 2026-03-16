using ERP_System.Application.Common;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(int ProductId, string PrdName, string? Description,
                                        decimal Price,decimal CostPrice ) : IRequest<ApiResponse<ProductResponse>>

    { 
    }
}