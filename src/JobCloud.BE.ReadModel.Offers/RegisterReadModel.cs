using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;
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
        public static void RegisterOffersReadModelsEndpoints(this WebApplication app, ISender sender)
        {
            app.MapGet("/offers", (object filters) => AggregateReadModels(sender));
        }

        private static async Task<IEnumerable<Offer>> AggregateReadModels(ISender sender)
        {
            var response = new List<Offer>();
            var readmodelsRequests = new List<IGetOffersRequest>();
            foreach (var request in readmodelsRequests)
            {
                var offers = await sender.Send(request);
                if (offers.Any())
                {
                    response.AddRange(offers);
                }
                else
                {
                    //some logging and exception handling
                }
            }
            return response;
        }
    }
}
