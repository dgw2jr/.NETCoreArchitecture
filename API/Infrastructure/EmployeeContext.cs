using System;
using System.Data;
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

    public class NHibernateEmployeeContext : IEmployeeContext, IDisposable
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;
        private bool _isAlive = true;

        public NHibernateEmployeeContext(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Add<T>(T entity) where T : class
        {
            _session.Save(entity);
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
            if (!_isAlive)
                return;

            try
            {
                _transaction.Commit();
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
            }
        }

        public IQueryable<Employee> Employees => _session.Query<Employee>();

        public void Dispose()
        {
            _session?.Dispose();
            _transaction?.Dispose();
        }
    }
}
