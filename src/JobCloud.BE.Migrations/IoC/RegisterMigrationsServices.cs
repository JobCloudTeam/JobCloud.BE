using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
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
            CreateDatabase(configuration);

            services.AddFluentMigratorCore()
                 .ConfigureRunner(config =>
                     config.AddSqlServer()
                     .WithGlobalConnectionString(configuration.GetConnectionString("JBCConnectionString"))
                     .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                 .Configure<RunnerOptions>(config =>
                 {
                     config.Tags = new[] { "JobCloud" };
                 })
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
                {
                    migrator.MigrateUp();
                }
            }
            return app;
        }

        private static void CreateDatabase(IConfiguration configuration)
        {
            var createDBprovider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString("BaseConnectionString"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                .Configure<RunnerOptions>(config =>
                {
                    config.Tags = new[] { "Server" };
                })
                .AddLogging(config =>
                    config.AddFluentMigratorConsole())
                .BuildServiceProvider();

            using (var scope = createDBprovider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}
