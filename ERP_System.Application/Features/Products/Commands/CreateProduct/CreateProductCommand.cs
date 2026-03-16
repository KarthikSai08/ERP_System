using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string SKU,
                                        string Description, decimal Price,
                                        decimal CostPrice, int CategoryId) : IRequest<ApiResponse<ProductResponseDto>>
    { }

}
