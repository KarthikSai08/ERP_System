using AutoMapper;
using ERP_System.Application.Common;
using ERP_System.Application.DTOs;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using MediatR;
using ERP_System.Application.Features.Categories.Commands.CreateCategory;
using System.Collections.Generic;
using System.Text;
using ERP_System.Domain.Exceptions;

namespace ERP_System.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponseDto>>
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoryResponseDto>> Handle(CreateCategoryCommand cmd, CancellationToken ct)
        {
            var exists = await _catRepo.ExistsByNameAsync(cmd.catName);
            if (exists)
                throw new ConflictException($"Category '{cmd.catName}' already exists");

            var cat = Category.Create(cmd.catName, cmd.description);
            await _catRepo.AddAsync(cat);

            var res = _mapper.Map<CategoryResponseDto>(cat);

            // ADD THESE DEBUG LINES
            Console.WriteLine($"cat is null: {cat is null}");
            Console.WriteLine($"res is null: {res is null}");
            Console.WriteLine($"res.CategoryId: {res?.CategoryId}");
            Console.WriteLine($"res.CategoryName: {res?.CategoryName}");

            return ApiResponse<CategoryResponseDto>.Ok(res);
        }

    }
}
