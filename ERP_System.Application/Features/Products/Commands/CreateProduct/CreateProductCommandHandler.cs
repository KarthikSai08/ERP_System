using ERP_System.Application.Common;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly ICategoryRepository _ctgRepo;
        private readonly IStockRepository _stkRepo;

        public CreateProductCommandHandler(IStockRepository stkRepo,IProductRepository prdRepo, ICategoryRepository ctgRepo)
        {
            _stkRepo = stkRepo;
            _prdRepo = prdRepo;
            _ctgRepo = ctgRepo;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand cmd, CancellationToken cancellationToken)
        {
            if (await _prdRepo.SkuExistAsync(cmd.SKU))
                throw new ConflictException($"Product with SKU '{cmd.SKU}' already exists ");

            var category = await _ctgRepo.GetByIdAsync(cmd.CategoryId)
                ?? throw new NotFoundException("Category", cmd.CategoryId);

            if (!category.IsActive)
                throw new ValidationException("Selected category is not active");

            var product = Product.Create(
                cmd.Name, cmd.SKU, cmd.Price,  cmd.CostPrice, cmd.CategoryId, cmd.Description);

            var id = await _prdRepo.AddAsync(product);

            return ApiResponse<ProductResponse>.Ok(
            new ProductResponse(
                id, product.ProductName, product.SKU, product.Description,
                product.Price, product.CostPrice,
                product.Price > 0 ? Math.Round(((product.Price - product.CostPrice) / product.Price) * 100, 2) : 0,
                category.CategoryName, product.IsActive, 0),
            "Product created successfully");
        }
    }
}
