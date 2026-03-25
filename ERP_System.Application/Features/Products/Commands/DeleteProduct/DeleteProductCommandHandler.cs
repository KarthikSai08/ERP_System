using ERP_System.Application.Common;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;

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
            var product = await _repo.GetByIdAsync(cmd.id, ct)
                ?? throw new NotFoundException("Product", cmd.id);

            if (!product.IsActive)
                return ApiResponse<bool>.Fail("Product is already inactive");

            product.Deactivate();

            await _repo.UpdateAsync(product, ct);

            return ApiResponse<bool>.Ok(true, "Product deactivated successfully");
        }
        
    }
}
