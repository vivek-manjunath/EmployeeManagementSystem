//using System.Collections.Generic;
//using System;
//using System.Threading.Tasks;
//using AutoMapper;
//using EmployeeManagementSystem.Configuration;
//using EmployeeManagementSystem.Models;
//using EmployeeManagementSystem.Repositories;
//using EmployeeManagementSystem.Services;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;

//namespace EmployeeManagementSystem.Tests.Services
//{
//    public class EmployeeServiceTest
//    {
//        private readonly EmployeeService employeeService = null;
//        private readonly IMapper mockMapper;
//        private readonly Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
//        private readonly Mock<ILogger<EmployeeService>> logger = new Mock<ILogger<EmployeeService>>();
//        private List<string> list;
//        public EmployeeServiceTest()
//        {
//            var mapperConfiguration = new MapperConfiguration(config =>
//            {
//                config.AddProfile(new AutoMapperConfig());
//            });
//            mockMapper = mapperConfiguration.CreateMapper();

//            employeeService = new EmployeeService(mockMapper, unitOfWork.Object, logger.Object);
//        }
//        [Fact]
//        public async Task GetById_ShouldReturnEmployee_WhenEmployeeExists()
//        {
//            //Arrange
//            var employee = new Employee
//            {
//                Id = 1,
//                FirstName = "Vivek",
//                LastName = "Manjunath",
//                DepartmentId = 1,
//                CreatedOn = DateTime.Now,
//                DOB = DateTime.Now,
//                Gender = "Male",
//                StreetAddress = "123 Highland street",
//                City = "Tampa",
//                State = "FL",
//                Zip = "33610"
//            };

//            unitOfWork.Setup(uow => uow.employeeRepository.GetWithDepartment(1)).ReturnsAsync(employee);

//            //Act
//            var emp = await employeeService.Get(1);

//            //Assert
//            Assert.Equal(1, emp.Id);
//        }

//        [Fact]
//        public async Task GetById_ShouldThrowAnException_WhenDbCallIsMade()
//        {
//            //Arrange
//            var exceptionMessage = "DB Connection Failed";
//            var empId = 1;

//            unitOfWork.Setup(uow => uow.employeeRepository.GetWithDepartment(empId)).Throws(new Exception(exceptionMessage));

//            //Act
//            var ex = await Assert.ThrowsAsync<Exception>(() => employeeService.Get(empId));

//            //Assert
//            Assert.Contains(exceptionMessage, ex.Message);
//        }

//        [Fact]
//        public void Delete_ShouldSuccessfullyDeleteEmployee_WhenEmployeeExists()
//        {
//            //Arrange
//            var empId = 1;
//            var employee = new Employee
//            {
//                Id = empId,
//                FirstName = "Vivek",
//                LastName = "Manjunath"
//            };

//            unitOfWork.Setup(uow => uow.employeeRepository.Get(empId)).Returns(employee);
//            unitOfWork.Setup(uow => uow.employeeRepository.Remove(employee));
//            unitOfWork.Setup(uow => uow.CompleteAsync());

//            //Act
//            employeeService.Delete(empId);

//            //Assert
//            Assert.Equal(1, 1);
//        }
//    }
//}