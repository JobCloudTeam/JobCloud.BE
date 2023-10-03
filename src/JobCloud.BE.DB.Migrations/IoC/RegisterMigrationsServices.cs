using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobCloued.BE.Migrations.IoC
{
    public static class RegisterMigrationsServices
    {
        public static IServiceCollection AddMigrationsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("BaseConnectionString"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                .AddLogging(config =>
                    config.AddFluentMigratorConsole());

            return services;
        }

        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
                if (migrator is not null)
                    migrator.ListMigrations();
            }
            return app;
        }
    }
}
