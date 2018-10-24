using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Domain;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IEmployeeContext _employeeContext;

        public ValuesController(IEmployeeContext employeeContext)
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
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CreateCustomerDto value)
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
