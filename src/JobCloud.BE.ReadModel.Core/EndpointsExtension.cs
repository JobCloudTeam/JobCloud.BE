using JobCloud.BE.ReadModel.Shared.Model;
using JobCloud.BE.ReadModel.Shared.Request;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System.Collections;

namespace JobCloud.BE.ReadModel.Core
{
    public static class EndpointsExtension
    {
        public static void RegisterReadModelsEndpoints(this WebApplication app, ISender sender)
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
                return offers;
            }
        }
    }
}
