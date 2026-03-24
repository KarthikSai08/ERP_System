using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponseDto>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        private readonly IMapper _mapper;
        public GetProductByIdHandler(IProductRepository prdRepo, IStockRepository stkRepo,IMapper mapper)
        {
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponseDto>> Handle(GetProductByIdQuery qry, CancellationToken ct)
        {
            var product =  _prdRepo.GetByIdAsync(qry.ProductId, ct);
            var stock =  _stkRepo.GetTotalStockAsync(qry.ProductId,ct);

            await Task.WhenAll(product, stock);

            var prd = await product ?? throw new NotFoundException("Product", qry.ProductId);

            var response = _mapper.Map<ProductResponseDto>(prd);
            response.TotalStock = await stock;

            return ApiResponse<ProductResponseDto>.Ok(response);
        }
    }
}
