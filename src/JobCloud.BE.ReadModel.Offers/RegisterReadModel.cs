using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;
using JobCloud.BE.ReadModel.Offers.RequestHandler;
using JobCloud.BE.ReadModel.Offers.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JobCloud.BE.ReadModel.Offers
{
    public static class RegisterReadModel
    {
        public static IServiceCollection RegisterOffersReadModelServices(this IServiceCollection services)
        {
            return services;
        }
        public static void RegisterOffersReadModelsEndpoints(this WebApplication app, IRequestsAggregator requestsAggregator)
        {
            app.MapGet("/offers", (object filters) => GetJobOffers(requestsAggregator, app.Services.GetService<ISender>()));
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
