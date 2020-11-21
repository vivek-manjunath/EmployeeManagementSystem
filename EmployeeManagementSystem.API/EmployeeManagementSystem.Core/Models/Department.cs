using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Department name cannot have more than 250 characters")]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdateOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}