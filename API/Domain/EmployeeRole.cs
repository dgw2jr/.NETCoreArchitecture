using System;
using System.Collections.Generic;

namespace Domain
{
    public class EmployeeRole : Entity
    {
        public virtual decimal Bonus { get; }
    }

    public class CEOEmployeeRole : EmployeeRole
    {
        public override Guid ID => Guid.Parse("{758C756A-27CF-43F4-A9EC-C5CB1E72E11E}");
        public override decimal Bonus => 100000m;
    }

    public class CIOEmployeeRole : EmployeeRole
    {
        public override Guid ID => Guid.Parse("{301DA9EE-14C2-4256-9835-5BD5DF431BAC}");
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