using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Application.Interfaces.Persistence;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly ILogger<EmployeeService> logger;
        private readonly IUnitOfWork unitOfWork;
        public EmployeeService(IMapper _mapper, IUnitOfWork _unitOfWork, ILogger<EmployeeService> _logger) =>
            (this.unitOfWork, this.logger, this.mapper) = (_unitOfWork, _logger, _mapper);
        

        public async void Create(Employee employee)
        {
            try
            {
                unitOfWork.employeeRepository.Add(employee);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void Delete(int id)
        {
            try
            {
                var employee = await unitOfWork.employeeRepository.GetAsync(id);
                unitOfWork.employeeRepository.Remove(employee);
                await unitOfWork.CompleteAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeDto> Get()
        {
            var emps = unitOfWork.employeeRepository.GetAllWithDepartment().OrderBy(e => e.FirstName);
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(emps);
        }

        public async Task<EmployeeDto> Get(int id)
        {
            try
            {
                var emp = await unitOfWork.employeeRepository.GetWithDepartment(id);
                return mapper.Map<EmployeeDto>(emp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeDto> SearchByName(string name)
        {
            var emps = new List<Employee>();
            if (string.IsNullOrEmpty(name))
            {
                emps = unitOfWork.employeeRepository.GetAll().OrderBy(e => e.FirstName).ToList();
            }
            else
            {
                emps = unitOfWork.employeeRepository.GetAll().Where(e => e.FirstName.ToLower().StartsWith(name.ToLower()) || e.LastName.ToLower().StartsWith(name.ToLower())).OrderBy(e => e.FirstName).ToList();
            }
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(emps);
        }

        public void Update(Employee employee)
        {
            try
            {
                unitOfWork.employeeRepository.Update(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}