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
    public class ValuesController : ControllerBase
    {
        private readonly IEmployeeContext _employeeContext;

        public ValuesController(IEmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return _employeeContext.Employees.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CreateCustomerDto value)
        {
            var employee = Employee.Create(value.Name, EmployeeRoles.RolesDictionary[value.Role]);
            if (employee.IsFailure)
            {
                return BadRequest(employee.Error);
            }

            _employeeContext.Add(employee.Value);
            _employeeContext.Save();

            return Ok();
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
