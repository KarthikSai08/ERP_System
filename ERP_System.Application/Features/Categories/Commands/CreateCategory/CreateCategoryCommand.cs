using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(string catName,string? description) : IRequest<ApiResponse<CategoryResponseDto>>
    {
    }
}
