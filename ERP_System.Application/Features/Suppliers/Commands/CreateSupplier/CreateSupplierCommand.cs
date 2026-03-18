using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Suppliers.Commands.CreateSupplier
{
    public record CreateSupplierCommand(string name, string contactPerson,string email, string phone,string address) : IRequest<ApiResponse<SupplierResponseDto>>;
}
