using JobCloud.BE.ReadModel.Offers.Db;
using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;
using JobCloud.BE.ReadModel.Offers.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.ReadModel.Offers
{
    public static class RegisterReadModel
    {
        public static IServiceCollection RegisterOffersReadModelServices(this IServiceCollection services)
        {
            services.AddScoped<IOffersRepository, OffersRepository>();
            services.AddScoped<IRequestsAggregator, RequestsAggregator>();
            return services;
        }
        public static void RegisterOffersReadModelsEndpoints(this WebApplication app)
        {
            app.MapGet("/api/readmodel/offers", ([FromServices] IRequestsAggregator requestAggregator, [FromServices] ISender sender) => GetJobOffers(requestAggregator, sender));
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
