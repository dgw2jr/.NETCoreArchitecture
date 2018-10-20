using System.Linq;

namespace Domain
{
    public interface IEmployeeContext
    {
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void Save();

        IQueryable<Employee> Employees { get; }
        IQueryable<EmployeeRole> EmployeeRoles { get; }
    }
}
