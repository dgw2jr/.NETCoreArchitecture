using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<EmployeeContext>(builder => builder.UseInMemoryDatabase("test"));
            services.AddScoped<IEmployeeContext>(provider => provider.GetService<EmployeeContext>());
        }
    }
}