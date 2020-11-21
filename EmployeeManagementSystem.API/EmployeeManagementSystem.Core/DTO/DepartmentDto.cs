using System;

namespace EmployeeManagementSystem.Core.Dto
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public int TotalEmployees { get; set; }
        public int Id { get; set; }
        public string CreatedOn { get; set; }
    }
}