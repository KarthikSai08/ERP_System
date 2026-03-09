using ERP_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Department { get; private set; }
        public string Designation { get; private set; }
        public decimal Salary { get; private set; }
        public DateTime JoiningDate { get; private set; }
        public EmployeeStatus Status { get; private set; }

        private Employee() { }

        public string FullName => $"{FirstName} {LastName}";

        public static Employee Create(string firstName, string lastName, string email,
            string phone, string department, string designation, decimal salary) => new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email.ToLower().Trim(),
                Phone = phone,
                Department = department,
                Designation = designation,
                Salary = salary,
                JoiningDate = DateTime.UtcNow,
                Status = EmployeeStatus.Active
            };

        public void UpdateSalary(decimal newSalary) => Salary = newSalary;
        public void Terminate() => Status = EmployeeStatus.Terminated;
        public void SetOnLeave() => Status = EmployeeStatus.OnLeave;
        public void Reactivate() => Status = EmployeeStatus.Active;
    }
}
