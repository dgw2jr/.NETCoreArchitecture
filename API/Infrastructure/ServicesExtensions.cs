using System;
using System.Configuration;
using System.Linq;
using Domain;
using Domain.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddDataServices(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<EmployeeContext>(builder => builder.UseInMemoryDatabase("test"));
            //services.AddScoped<IEmployeeContext>(provider => provider.GetService<EmployeeContext>());
            var configuration = Fluently.Configure().BuildConfiguration();
            var factory = Fluently.Configure(configuration)
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Employee>(new StoreConfiguration())))
                .BuildSessionFactory();

            //new SchemaExport(configuration).Create(false, true);

            services.AddSingleton(factory);
            services.AddScoped(provider => provider.GetService<ISessionFactory>().OpenSession());
            services.AddScoped<IEmployeeContext>(provider => new NHibernateEmployeeContext(provider.GetService<ISession>()));

            using (var session = factory.OpenSession())
            {
                if (!session.Query<EmployeeRole>().Any())
                {
                    foreach (var employeeRole in EmployeeRoles.Roles)
                    {
                        session.SaveOrUpdate(employeeRole);
                    }
                }
            }
        }
    }

    internal class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "Domain.Entities";
        }

        public override bool IsComponent(Type type)
        {
            return type.Namespace == "Domain.ValueObjects";
        }
    }
}