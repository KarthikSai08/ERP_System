using ERP_System.Application.Common;
using ERP_System.Application.Features.Products.Commands.CreateProduct;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>
    {
        private readonly IProductRepository _prdRepo;
        private readonly IStockRepository _stkRepo;
        public GetProductByIdHandler(IProductRepository prdRepo, IStockRepository stkRepo)
        {
            _prdRepo = prdRepo;
            _stkRepo = stkRepo;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery qry, CancellationToken ct)
        {
            var p = await _prdRepo.GetByIdAsync(qry.ProductId)
                ?? throw new NotFoundException("Product", qry.ProductId);

            var stock = await _stkRepo.GetTotalStockAsync(qry.ProductId);

            return 
        }
    }
}
