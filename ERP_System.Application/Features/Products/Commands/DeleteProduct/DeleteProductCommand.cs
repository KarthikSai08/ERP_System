using ERP_System.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int id) : IRequest<ApiResponse<bool>>
    {
    }
}
