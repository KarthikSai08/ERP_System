using ERP_System.Application.Common;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
    {
        private readonly IProductRepository _repo;
        public DeleteProductCommandHandler(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApiResponse<bool>> Handle(DeleteProductCommand cmd,CancellationToken ct)
        {
            var prd = await _repo.GetByIdAsync(cmd.id, ct)
                ?? throw new NotFoundException("Product", cmd.id);

            prd.Deactivate();
            await _repo.UpdateAsync(prd, ct);

            return ApiResponse<bool>.Ok(true, "Product deactivated Successfully");
        }
        
    }
}
