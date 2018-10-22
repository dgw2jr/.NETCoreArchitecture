using System.Linq;
using Domain.Entities;

namespace Domain
{
    public interface IEmployeeContext
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void Save();

        IQueryable<Employee> Employees { get; }
    }
}
