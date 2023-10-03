using JobCloud.BE.ReadModel.Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.ReadModel.Db.IoC
{
    public static class RegisterServices
    {
        public static IServiceCollection AddReadModelDbServices(this IServiceCollection services)
        {
            services.AddScoped<IOffersRepository, OffersRepository>();
            return services;
        }
    }
}
