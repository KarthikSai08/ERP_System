using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<ProductResponseDto>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductRepository prdRepo, IStockRepository stkRepo,IMapper mapper)
        {
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponseDto>> Handle(UpdateProductCommand cmd, CancellationToken ct)
        {
            var prd = await _prdRepo.GetByIdAsync(cmd.ProductId,ct)
                ?? throw new NotFoundException("Product", cmd.ProductId);

            prd.Update(cmd.PrdName, cmd.Description, cmd.Price, cmd.CostPrice);
            await _prdRepo.UpdateAsync(prd,ct);

            var response = _mapper.Map<ProductResponseDto>(prd);
            var totalStock = await _stkRepo.GetTotalStockAsync(cmd.ProductId,ct);

            return ApiResponse<ProductResponseDto>.Ok(response,"Prodct Updated Successfully");
        }
    }
}
