using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ERP_System.Application.Features.Inventory.Query
{
    public class GetLowStockAlertQueryHandler : IRequestHandler<GetLowstockAlertQuery, ApiResponse<IEnumerable<LowStockAlertResponse>>>
    {
        private readonly IStockRepository _stkRepo;
        private readonly IMapper _mapper;

        public GetLowStockAlertQueryHandler(IStockRepository stkRepo, IMapper mapper)
        {
            _stkRepo = stkRepo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<LowStockAlertResponse>>> Handle(GetLowstockAlertQuery qry, CancellationToken ct)
        {
            var stock = await _stkRepo.GetLowStockItemsAsync(ct);
            var res = _mapper.Map<IEnumerable<LowStockAlertResponse>>(stock);

            return ApiResponse<IEnumerable<LowStockAlertResponse>>.Ok(res);
        }
    }
}
