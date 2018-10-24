using System;
using System.Configuration;
using System.Linq;
using Domain;
using Domain.Entities;
using Domain.ValueObjects;
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
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
                .BuildSessionFactory();

            new SchemaExport(configuration).Create(false, true);

            services.AddSingleton(factory);
            services.AddScoped<IEmployeeContext>(provider => new NHibernateEmployeeContext(provider.GetService<ISessionFactory>()));

        }
    }
}