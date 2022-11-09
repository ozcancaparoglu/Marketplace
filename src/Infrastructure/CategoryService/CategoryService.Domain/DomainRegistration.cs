using CategoryService.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocdata.Operations.Cache.Redis;

namespace CategoryService.Domain
{
    public static class DomainRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("CategoryConnectionString")));

            services.Configure<RedisConfigurationOptions>(configuration.GetSection("RedisDatabase"));

            services.AddScoped<DbContext, AppDbContext>();

            return services;
        }
    }
}
