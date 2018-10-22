using System.Collections.Generic;

namespace Domain.Entities
{
    public class EmployeeRole : Entity
    {
        public virtual decimal Bonus { get; }
    }

    public class CEOEmployeeRole : EmployeeRole
    {
        public override decimal Bonus => 100000m;
    }

    public class CIOEmployeeRole : EmployeeRole
    {
        public override decimal Bonus => 10000m;
    }

    public static class EmployeeRoles
    {
        public static CEOEmployeeRole CEO => new CEOEmployeeRole();
        public static CIOEmployeeRole CIO => new CIOEmployeeRole();
        public static List<EmployeeRole> Roles => new List<EmployeeRole>
        {
            CEO,
            CIO
        };
    }
}