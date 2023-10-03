using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.Application.IoC
{
    public static class RegisterApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
