using System;
using CSharpFunctionalExtensions;

namespace Domain
{
    public class Employee : Entity
    {
        protected Employee()
        {
            
        }

        private Employee(string name, EmployeeRole role)
        {
            Name = name;
            EmployeeRole = role;
        }

        public string Name { get; private set; }

        public static Result<Employee> Create(string name, EmployeeRole role)
        {
            if (role == null) return Result.Fail<Employee>("Role cannot be null.");
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail<Employee>("Name cannot be null or whitespace");

            return Result.Ok(new Employee(name, role));
        }

        public EmployeeRole EmployeeRole { get; private set; }
    }
}