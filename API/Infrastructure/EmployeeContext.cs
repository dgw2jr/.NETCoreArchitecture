using System.Linq;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NHibernate;

namespace Infrastructure
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

        public void Update<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
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

        protected DbSet<Employee> EmployeeSet { get; set; }

    }

    public class NHibernateEmployeeContext : IEmployeeContext
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public NHibernateEmployeeContext(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public void Add<T>(T entity) where T : class
        {
            _session.SaveOrUpdate(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _session.SaveOrUpdate(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _session.Delete(entity);
        }

        public void Save()
        {
            _transaction.Commit();
        }

        public IQueryable<Employee> Employees => _session.Query<Employee>();

    }
}
