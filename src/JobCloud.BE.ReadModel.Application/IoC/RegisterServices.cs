using JobCloud.BE.ReadModel.Application.Request;
using JobCloud.BE.ReadModel.Application.Services;
using JobCloud.BE.ReadModel.Core.Model;
using JobCloud.BE.ReadModel.Db.IoC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobCloud.BE.ReadModel.Application.IoC
{
    public static class RegisterServices
    {
        public static IServiceCollection AddReadModelServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRequestsAggregator, RequestsAggregator>();
            services.AddReadModelDbServices();

            return services;
        }

        public static void RegisterOffersReadModelsEndpoints(this WebApplication app)
        {
            app.MapGet("/api/readmodel/offers", GetJobOffers);
        }

        private static async Task<IEnumerable<Offer>> GetJobOffers(IRequestsAggregator requestsAggregator, ISender sender)
        {
            var requests = new List<IRequest<IEnumerable<Offer>>>
            {
                new GetNoFluffJobsRequest(),
                new GetJustJoinItOffersRequest()
            };
            return await requestsAggregator.AggregateJobOffers(requests, sender);
        }
    }
}
