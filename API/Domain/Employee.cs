namespace Domain
{
    public class Employee : Entity
    {
        public string Name { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}