using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ApiResponse<IEnumerable<CustomerResponseDto>>>
    {
        private readonly ICustomerRepository _cstRepo;
        private readonly IMapper _mapper;
        public GetAllCustomersQueryHandler(IMapper mapper, ICustomerRepository cstRepo)
        {
            _mapper = mapper;
            _cstRepo = cstRepo;
        } 

        public async Task<ApiResponse<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomersQuery qry, CancellationToken ct)
        {
            var customers = await _cstRepo.GetAllAsync(ct);

            var res = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

            return ApiResponse<IEnumerable<CustomerResponseDto>>.Ok(res);
        }
    }
}
