using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Features.Orders.Queries.GetSalesReport
{
    public class GetSalesReportQueryValidator : AbstractValidator<GetSalesReportQuery>
    {
        public GetSalesReportQueryValidator()
        { 
            RuleFor(x => x.From).LessThan(x => x.To).WithMessage("From date must be before To date");
        }
    }
}
