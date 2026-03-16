using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Valid product Id is Required");

            RuleFor(x => x.PrdName)
                .NotEmpty().MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Prices should not be in negative values");

            RuleFor(x => x.CostPrice)
                .GreaterThanOrEqualTo(0);
                
        }
    }
}
