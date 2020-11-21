using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Services.Interfaces;
using System;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Models;

namespace EmployeeManagementSystem.Web.Controllers
{

    [Authorize]
    public class EmployeeController : BaseApiController
    {
        private readonly ILogger<EmployeeController> logger;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public EmployeeController(ILogger<EmployeeController> _logger, IMapper _mapper, IEmployeeService _employeeService) =>
            (this.employeeService, this.mapper, this.logger) = (_employeeService, _mapper, _logger);

        // GET: api/Employee
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> Get() => Ok(employeeService.Get());

        // GET: api/Employee/5
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var emp = await employeeService.Get(id);
            return Ok(emp);
        }

        // GET: api/Employee/name
        [Route("search/{name?}")]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDto>> Search(string name)
        {
            var emps = employeeService.SearchByName(name);
            return Ok(emps);
        }

        // POST: api/Employee
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Post(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            try
            {
                employeeService.Create(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Created("New employee created", employee);
        }

        // PUT: api/Employee
        [HttpPut]
        [Authorize]
        public ActionResult Put(Employee employee)
        {
            try
            {
                employeeService.Update(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Accepted();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = employeeService.Get(id);
                if (employee is null)
                {
                    NotFound();
                }
                employeeService.Delete(id);
                return Accepted();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}