using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class CategoryResponseDto
    {
        public int CategoryId { get;  set; }
        public string CategoryName { get;  set; }
        public string? Description { get;  set; }
        public bool IsActive { get;  set; }
        public DateTime CreatedAt { get;  set; }

    }
}
