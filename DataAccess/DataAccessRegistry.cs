using DataAccess.Repositories.Implementation;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public class DataAccessRegistry
    {
        public DataAccessRegistry()
        {
        }

        public static void RegisterComponents(IServiceCollection services, IConfigurationRoot configuration)
        {
            var connection = configuration.GetSection("Data").GetSection("DefaultConnection").GetSection("ConnectionString").Value.ToString();

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<studentsContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IStudentInfoRepo, StudentInfoRespository>();
            services.AddScoped<IDepartmentRepo, DepartmentRespository>();
        }
    }
}
