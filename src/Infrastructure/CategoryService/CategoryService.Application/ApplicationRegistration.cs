using CategoryService.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CategoryService.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAsyncCategoryService, AsyncCategoryService>();
            services.AddScoped<IAsyncCategoryAttributeService, AsyncCategoryAttributeService>();

            return services;
        }
    }
}
