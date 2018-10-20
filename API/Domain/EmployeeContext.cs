using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class EmployeeContext : DbContext, IEmployeeContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
            
        }

        public new void Add<T>(T entity) where T : class 
        {
            Set<T>().Add(entity);
        }

        public new void Remove<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        public void Save()
        {
            SaveChanges();
        }

        public IQueryable<Employee> Employees => EmployeeSet.AsQueryable();
        public IQueryable<EmployeeRole> EmployeeRoles => EmployeeRoleSet.AsQueryable();

        protected DbSet<Employee> EmployeeSet { get; set; }
        protected DbSet<EmployeeRole> EmployeeRoleSet { get; set; }

    }
}
