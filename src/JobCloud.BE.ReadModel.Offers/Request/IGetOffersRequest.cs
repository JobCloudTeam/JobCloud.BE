using JobCloud.BE.ReadModel.Offers.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.Request
{
    public interface IGetOffersRequest : IRequest<IEnumerable<Offer>>
    {
        public object Filters { get; set; }
    }
}
