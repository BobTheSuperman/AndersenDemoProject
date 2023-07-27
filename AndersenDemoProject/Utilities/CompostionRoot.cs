using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AndersenDemoProject.Utilities
{
    public static class CompostionRoot
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region custom service dependencies

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            #endregion 
        }
    }
}
