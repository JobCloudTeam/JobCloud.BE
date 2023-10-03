using JobCloud.BE.ReadModel.Core.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Application.Services
{
    public interface IRequestsAggregator
    {
        Task<IEnumerable<Offer>> AggregateJobOffers(IEnumerable<IRequest<IEnumerable<Offer>>> requests, ISender sender);
    }
}
