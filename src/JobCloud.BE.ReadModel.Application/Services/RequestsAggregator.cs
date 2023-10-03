using JobCloud.BE.ReadModel.Core.Model;
using MediatR;
using System.Collections.Concurrent;

namespace JobCloud.BE.ReadModel.Application.Services
{
    public class RequestsAggregator : IRequestsAggregator
    {
        public async Task<IEnumerable<Offer>> AggregateJobOffers(IEnumerable<IRequest<IEnumerable<Offer>>> requests, ISender sender)
        {
            var offers = new ConcurrentBag<Offer>();
            var tasks = new List<Task>();
            foreach (var request in requests)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var requestOffers = await sender.Send(request);
                    foreach(var offer in requestOffers)
                    {
                        offers.Add(offer);
                    }
                }));
            }
            await Task.WhenAll(tasks);

            return offers.ToList();
        }
    }
}
