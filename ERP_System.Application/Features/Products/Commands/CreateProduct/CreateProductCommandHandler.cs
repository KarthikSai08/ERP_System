using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponseDto>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly ICategoryRepository _ctgRepo;
        private readonly IStockRepository _stkRepo;
        private readonly IMapper _map;

        public CreateProductCommandHandler(IStockRepository stkRepo,IProductRepository prdRepo, ICategoryRepository ctgRepo,IMapper map)
        {
            _stkRepo = stkRepo;
            _prdRepo = prdRepo;
            _ctgRepo = ctgRepo;
            _map = map;
        }

        public async Task<ApiResponse<ProductResponseDto>> Handle(CreateProductCommand cmd, CancellationToken cancellationToken)
        {
            if (await _prdRepo.SkuExistAsync(cmd.SKU,cancellationToken))
                throw new ConflictException($"Product with SKU '{cmd.SKU}' already exists ");

            var category = await _ctgRepo.GetByIdAsync(cmd.CategoryId)
                ?? throw new NotFoundException("Category", cmd.CategoryId);

            if (!category.IsActive)
                throw new ValidationException("Selected category is not active");

            var product = Product.Create(
                cmd.Name, cmd.SKU, cmd.Price,  cmd.CostPrice, cmd.CategoryId, cmd.Description);

            product.SetCategory(category);

            await _prdRepo.AddAsync(product,cancellationToken);

            var response = _map.Map<ProductResponseDto>(product);

            response.TotalStock = 0;

            return ApiResponse<ProductResponseDto>.Ok(response,"Product created successfully");
        }
    }
}
