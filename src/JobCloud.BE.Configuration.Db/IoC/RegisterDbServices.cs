using JobCloud.BE.Configuration.Db.Factories;
using JobCloud.BE.Configuration.Db.Repositories;
using JobCloud.BE.Configuration.Db.Repositories.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.Configuration.Db.IoC
{
    public static class RegisterDbServices
    {
        public static IServiceCollection AddConfigurationDbServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));
            services.AddScoped<IJustJoinItRepository, JustJoinItRepository>();

            return services;
        } 
    }
}
