using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string Designation { get; set; } = default!;
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Status { get; set; } = default!;
    }
}
