using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.Services
{
    public class RequestsAggregator : IRequestsAggregator
    {
        public Task<IEnumerable<Offer>> AggregateJobOffers(IEnumerable<IRequest<IEnumerable<Offer>>> requests, ISender sender)
        {


            throw new NotImplementedException();
        }
    }
}
