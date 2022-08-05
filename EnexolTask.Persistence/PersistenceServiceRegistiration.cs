using EnexolTask.Application.Persistence.Contract.Persistence;
using EnexolTask.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Persistence
{
    public static class PersistenceServiceRegistiration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));
            return services;
        }
    }
}
