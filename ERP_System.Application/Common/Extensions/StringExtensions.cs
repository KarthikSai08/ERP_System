using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static string OrNA(this string? value) =>
            string.IsNullOrWhiteSpace(value) ? "N/A" : value;
    }
}
