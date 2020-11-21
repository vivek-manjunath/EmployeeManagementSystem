using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Core.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "First Name cannot have more than 250 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Last Name cannot have more than 250 characters")]
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}