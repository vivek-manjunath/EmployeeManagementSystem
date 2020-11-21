using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Hubs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using EmployeeManagementSystem.Services.Interfaces;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Core.Dto;

namespace EmployeeManagementSystem.Web.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly ILogger<DepartmentController> logger;
        private readonly IHubContext<DashboardHub> hubContext;
        private readonly IDepartmentService departmentService;

        public DepartmentController(ILogger<DepartmentController> _logger, IHubContext<DashboardHub> _hubContext, IDepartmentService _departmentService)
            => (this.departmentService, this.hubContext, this.logger) = (_departmentService, _hubContext, _logger);
        

        // GET: api/department
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<Department>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<DepartmentDto>> Get()
        {
            return Ok(departmentService.Get());
        }

        // GET: api/department/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<Department> Get(int id)
        {
            return Ok(departmentService.Get(id));
        }

        // POST: api/department
        [HttpPost]
        [Authorize]
        public IActionResult Post(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            try
            {
                departmentService.Create(department);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Created("", department);
        }

        // PUT: api/Employee
        [HttpPut]
        [Authorize]
        public ActionResult Put(Department department)
        {
            try
            {
                departmentService.Update(department);
            }
            catch (System.Exception)
            {

                throw;
            }

            return Accepted();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                departmentService.Delete(id);
                return Accepted();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}