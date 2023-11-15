using JobCloud.BE.Shared.Models;
using MediatR;

namespace JobCloud.BE.JustJoinIt.Application.Read.GetOffers
{
    public class GetOffersRequest : IRequest<IEnumerable<BaseOffer>>
    {
        public OfferFIlters Filters { get; set; }
    }
}
