using CSharpFunctionalExtensions;
using Domain.ValueObjects;

namespace Domain.Entities
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

        public virtual string Name { get; protected set; }

        public static Result<Employee> Create(string name, EmployeeRole role)
        {
            if (role == null) return Result.Fail<Employee>("Role cannot be null.");
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail<Employee>("Name cannot be null or whitespace");

            return Result.Ok(new Employee(name, role));
        }

        public virtual EmployeeRole EmployeeRole { get; protected set; }
    }
}