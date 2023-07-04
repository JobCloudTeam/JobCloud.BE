using JobCloud.BE.ReadModel.Offers.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.Services
{
    public interface IRequestsAggregator
    {
        Task<IEnumerable<Offer>> AggregateJobOffers(IEnumerable<IRequest<IEnumerable<Offer>>> requests, ISender sender);
    }
}
