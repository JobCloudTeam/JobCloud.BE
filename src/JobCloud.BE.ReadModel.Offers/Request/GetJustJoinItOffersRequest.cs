using JobCloud.BE.ReadModel.Offers.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.Request
{
    public class GetJustJoinItOffersRequest : IRequest<IEnumerable<Offer>>
    {
        public object Filters { get; }
    }
}
