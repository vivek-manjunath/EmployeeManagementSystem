using EmployeeManagementSystem.Application.Interfaces.Persistence;
using EmployeeManagementSystem.Application.Services;
using EmployeeManagementSystem.Repositories;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Configuration
{
    public static class DIConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // services.AddScoped<IEmployeeRepository>();
            // services.AddScoped<IDepartmentRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<DashboardService>();

            return services;
        }
    }
}