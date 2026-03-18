using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, ApiResponse<IEnumerable<OrderResponseDto>>>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<OrderResponseDto>>> Handle(GetAllOrdersQuery qry, CancellationToken ct)
        {
            var orders = await _orderRepo.GetAllAsync(ct);
            var res = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);

            return ApiResponse<IEnumerable<OrderResponseDto>>.Ok(res);
        }
    }
}
