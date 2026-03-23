using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator() 
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.newStatus).IsInEnum().WithMessage("Invalid Order Status");
        }
    }
}
