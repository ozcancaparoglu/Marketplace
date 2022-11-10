using AttributeService.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AttributeService.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAsyncAttributeService, AsyncAttributeService>();
            services.AddScoped<IAsyncAttributeValueService, AsyncAttributeValueService>();
            services.AddScoped<IAsyncUnitService, AsyncUnitService>();

            return services;
        }
    }
}
