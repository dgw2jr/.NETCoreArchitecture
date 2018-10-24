using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Domain.ValueObjects
{
    public class EmployeeRole : ValueObject
    {
        public virtual string Name { get; set; }
        public virtual decimal Bonus { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Bonus;
            yield return Name;
        }
    }

    public class CEOEmployeeRole : EmployeeRole
    {
        public override string Name => "CEO";
        public override decimal Bonus => 100000m;
    }

    public class CIOEmployeeRole : EmployeeRole
    {
        public override string Name => "CIO";
        public override decimal Bonus => 10000m;
    }

    public static class EmployeeRoles
    {
        public static CEOEmployeeRole CEO => new CEOEmployeeRole();
        public static CIOEmployeeRole CIO => new CIOEmployeeRole();
        public static List<EmployeeRole> Roles => RolesDictionary.Select(d => d.Value).ToList();

        public static Dictionary<string, EmployeeRole> RolesDictionary => new Dictionary<string, EmployeeRole>
        {
            { "CEO", CEO },
            { "CIO", CIO }
        };
    }
}