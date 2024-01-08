using JobCloud.BE.ReadModel.Db.Factories;
using JobCloud.BE.ReadModel.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.ReadModel.Db.IoC
{
    public static class RegisterServices
    {
        public static IServiceCollection AddReadModelDbServices(this IServiceCollection services)
        {
            services.AddSingleton<DbContextFactory>();

            services.AddScoped<IOffersRepository, OffersRepository>();
            return services;
        }
    }
}
