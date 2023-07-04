using JobCloud.BE.ReadModel.Offers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.Application.IoC
{
    public static class RegisterApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(GetHandlersAssemblies());
            return services;
        }

        private static Type[] GetHandlersAssemblies()
        {
            var types = new Type[]
            {
                typeof(RegisterReadModel),  //ReadModel.Offers
            };
            return types;
        }
    }
}
