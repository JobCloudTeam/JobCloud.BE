using JobCloud.BE.JustJoinIt.Application.Write.Services;
using JobCloud.BE.JustJoinIt.Application.Write.Services.Imp;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobCloud.BE.JustJoinIt.Application.IoC
{
    public static class RegisterApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IOfferUrlScrapperService, OfferUrlScrapperService>();

            return services;
        }
    }
}
