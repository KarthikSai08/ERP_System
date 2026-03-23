using ERP_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class EmployeeResponseDto
    {
        public int EmployeeId { get;   set; }
        public string FirstName { get;   set; }
        public string LastName { get;   set; }
        public string Email { get;   set; }
        public string Phone { get;   set; }
        public string Department { get;   set; }
        public string Designation { get;   set; }
        public decimal Salary { get;   set; }
        public DateTime JoiningDate { get;   set; }
        public EmployeeStatus Status { get;   set; }
    }
}
