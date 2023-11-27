using JobCloud.BE.JustJoinIt.Db.Factories;
using JobCloud.BE.JustJoinIt.Db.Repositories;
using JobCloud.BE.JustJoinIt.Db.Repositories.Imp;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.JustJoinIt.Db.IoC
{
    public static class RegisterDbServices
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddSingleton<DbContextFactory>();

            services.AddScoped<IOfferRepository, OfferRepository>();

            return services;
        }
    }
}
