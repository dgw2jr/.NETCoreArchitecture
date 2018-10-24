﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Domain.ValueObjects
{
    public class EmployeeRole : ValueObject
    {
        public virtual decimal Bonus { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Bonus;
        }
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