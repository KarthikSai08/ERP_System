using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Inventory.Query;
using ERP_System.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ERP_System.Application.Features.Suppliers.Queries.GetAllSppliers
{
    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, ApiResponse<IEnumerable<SupplierResponseDto>>>
    {
        private readonly ISupplierRepository _supRepo;
        private readonly IMapper _mapper;

        public GetAllSuppliersQueryHandler(ISupplierRepository supRepo, IMapper mapper)
        {
            _supRepo = supRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<SupplierResponseDto>>> Handle(GetAllSuppliersQuery qry, CancellationToken ct)
        {
            var suppliers = await _supRepo.GetAllAsync(ct);
            var res = _mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers);

            return ApiResponse<IEnumerable<SupplierResponseDto>>.Ok(res);
        }

    }
}
