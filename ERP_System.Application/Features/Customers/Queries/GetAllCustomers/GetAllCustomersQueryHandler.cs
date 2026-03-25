using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ApiResponse<PagedResponse<CustomerResponseDto>>>
    {
        private readonly ICustomerRepository _cstRepo;
        private readonly IMapper _mapper;
        public GetAllCustomersQueryHandler(IMapper mapper, ICustomerRepository cstRepo)
        {
            _mapper = mapper;
            _cstRepo = cstRepo;
        } 

        public async Task<ApiResponse<PagedResponse<CustomerResponseDto>>> Handle(GetAllCustomersQuery qry, CancellationToken ct)
        {
            var baseQry = _cstRepo.GetQueryable()
                .Where(c => c.IsActive);

            var totalCount =await baseQry.CountAsync(ct);
            var customers = await baseQry
                .OrderBy(c => c.CustomerId)
                .Skip((qry.PageNumber - 1) * qry.PageSize)
                .Take(qry.PageSize)
                .ToListAsync();

            var res = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            var paged = new PagedResponse<CustomerResponseDto>(
                res,
                totalCount,
                qry.PageNumber,
                qry.PageSize
            );
            return ApiResponse<PagedResponse<CustomerResponseDto>>.Ok(paged);
        }
    }
}