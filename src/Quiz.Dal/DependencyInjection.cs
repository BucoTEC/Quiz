using Quiz.Dal.Data;
using Quiz.Dal.Repositories.Uow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Dal
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(p => p.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
