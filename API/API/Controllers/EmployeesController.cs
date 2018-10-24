using System;
using System.Linq;
using API.DTOs;
using Domain;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeContext _employeeContext;

        public EmployeesController(IEmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeContext.Employees.ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeContext.Employees.SingleOrDefault(e => e.ID.Equals(id));
            
            if(employee == null)
                return Error("Employee not found.");
            
            return Ok(employee);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CreateEmployeeDto value)
        {
            var employee = Employee.Create(value.Name, EmployeeRoles.RolesDictionary.TryGetValue(value.Role, out var role) ? role : null);
            if (employee.IsFailure)
            {
                return BadRequest(Envelope.Error(employee.Error));
            }

            _employeeContext.Add(employee.Value);
            _employeeContext.Save();

            return Ok("Success");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
