using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Core.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Employee>().HasData(
            //     new Employee() { Id = 1, FirstName = "Steve", LastName = "Adams" },
            //     new Employee() { Id = 2, FirstName = "Steph", LastName = "Curry" },
            //     new Employee() { Id = 3, FirstName = "Kevin", LastName = "Durant" },
            //     new Employee() { Id = 4, FirstName = "Dwayne", LastName = "Wade" },
            //     new Employee() { Id = 5, FirstName = "Chris", LastName = "Paul" },
            //     new Employee() { Id = 6, FirstName = "Chris", LastName = "Bosh" },
            //     new Employee() { Id = 7, FirstName = "Mario", LastName = "Chalmers" },
            //     new Employee() { Id = 8, FirstName = "Kyrie", LastName = "Irving" },
            //     new Employee() { Id = 9, FirstName = "Kevin", LastName = "Love" },
            //     new Employee() { Id = 10, FirstName = "Kobe", LastName = "Bryant" },
            //     new Employee() { Id = 11, FirstName = "Steve", LastName = "Nash" },
            //     new Employee() { Id = 12, FirstName = "Kevin", LastName = "Garnet" },
            //     new Employee() { Id = 13, FirstName = "Danny", LastName = "Green" },
            //     new Employee() { Id = 14, FirstName = "Steve", LastName = "Adams" },
            //     new Employee() { Id = 15, FirstName = "Jason", LastName = "Taytum" }
            // );
        }
    }
}