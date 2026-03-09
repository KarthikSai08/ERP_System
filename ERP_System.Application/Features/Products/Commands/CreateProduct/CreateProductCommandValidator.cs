using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
    
        public CreateProductCommandValidator() 
        {
        
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product Name is Required")
                .MaximumLength(50).WithMessage("Name Cannot be Too Long!");

            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("Sku is required")
                .MaximumLength(50)
                .Matches("^[A-Z0-9-]+$").WithMessage("SKU must be uppercase Letters,numbers, hyphens only")
                .When(x => !string.IsNullOrEmpty(x.SKU));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Prices must be greater than Zero");

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Cost price cannot be Negative");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Valid Category is Required");
        }
    
    }
}
