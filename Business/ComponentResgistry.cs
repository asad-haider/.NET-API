using Business.Interfaces;
using Business.ServiceImplementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business
{
    public class ComponentResgistry
    {
        public static void RegisterComponents(IServiceCollection services)
        {
            //This is where all the dependency resolution will be done.
            services.AddScoped<IStudent, StudentService>();
            services.AddScoped<IDepartment, DepartmentService>();
        }
    }
}
