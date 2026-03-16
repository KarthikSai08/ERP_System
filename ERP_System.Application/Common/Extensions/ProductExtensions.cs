using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Common.Extensions
{
    public static class ProductExtensions
    { 
        public static decimal CalculateMargin(this Product prd) => 
            prd.Price > 0
                ? Math.Round(((prd.Price - prd.CostPrice) / prd.Price) * 100, 2) : 0;
    }
}
