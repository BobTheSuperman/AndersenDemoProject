using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Business.Blogs;
using Infrastructure.Business.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces.Blogs;
using Services.Interfaces.Users;

namespace AndersenDemoProject.Utilities
{
    public static class CompostionRoot
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
            });

            #region custom service dependencies

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogService, BlogService>();

            #endregion 
        }
    }
}
