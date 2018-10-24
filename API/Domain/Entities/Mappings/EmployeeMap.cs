using FluentNHibernate.Mapping;

namespace Domain.Entities.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(e => e.ID);
            Map(m => m.Name);
            Component(c => c.EmployeeRole, part => part.Map(role => role.Bonus));
        }
    }
}