using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagementSystem.Application.Interfaces.Persistence;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Errors;
using EmployeeManagementSystem.Hubs;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
        private readonly IHubContext<DashboardHub> hubContext;
        private readonly DashboardService dashboardService;

        public DepartmentService(IMapper _mapper, IUnitOfWork _unitOfWork, ILogger<DepartmentService> _logger, IHubContext<DashboardHub> _hubContext, DashboardService _dashboardService)
        {
            this.unitOfWork = _unitOfWork;
            this.dashboardService = _dashboardService;
            this.hubContext = _hubContext;
            this.logger = _logger;
            this.mapper = _mapper;
        }
        public async void Create(Department department)
        {
            department.CreatedOn = DateTime.Now;
            try
            {
                unitOfWork.departmentRepository.Add(department);
                await unitOfWork.CompleteAsync();
                RefreshDashboard();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public async void Delete(int id)
        {
            _ = Get(id);
            var department = await unitOfWork.departmentRepository.GetAsync(id);
            unitOfWork.departmentRepository.Remove(department);
            await unitOfWork.CompleteAsync();
            RefreshDashboard();
        }

        public IEnumerable<DepartmentDto> Get()
        {
            var deps = unitOfWork.departmentRepository.GetAll().OrderBy(e => e.Name);
            return mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(deps);
        }

        public async Task<DepartmentDto> Get(int id)
        {
            try
            {
                var dep = await unitOfWork.departmentRepository.GetAsync(id);
                _ = dep ?? throw new RestException(HttpStatusCode.NotFound, new { Department = "Not found" });
                return mapper.Map<DepartmentDto>(dep);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }

        public void Update(Department department)
        {
            try
            {
                unitOfWork.departmentRepository.Update(department);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public IEnumerable<DepartmentDto> SearchByName(string name)
        {
            var deps = unitOfWork.departmentRepository.GetAll().Where(d => d.Name.Contains(name)).OrderBy(e => e.Name);
            return mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(deps);
        }

        private async void RefreshDashboard()
        {
            using (var hub = new DashboardHub(hubContext))
            {
                await hub.RefreshDashboard(dashboardService.Get());
            }
        }
    }
}