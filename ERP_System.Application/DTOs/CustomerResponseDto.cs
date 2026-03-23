using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
