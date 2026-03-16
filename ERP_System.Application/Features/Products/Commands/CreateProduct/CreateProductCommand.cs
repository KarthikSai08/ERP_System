using ERP_System.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string SKU,
                                        string Description, decimal Price,
                                        decimal CostPrice, int CategoryId) : IRequest<ApiResponse<ProductResponse>>
    { }


    public record ProductResponse(int Id,string Name,
                                 string SKU,string? Description,
                                 decimal Price,decimal CostPrice,
                                 decimal MarginPercent,string CategoryName,
                                 bool IsActive,int TotalStock)
    { }

}
