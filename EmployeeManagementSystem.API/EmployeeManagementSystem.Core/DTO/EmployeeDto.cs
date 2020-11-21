using System;

namespace EmployeeManagementSystem.Core.Dto
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
        public string DOB { get; set; }
    }
}