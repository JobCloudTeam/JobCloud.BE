using JobCloud.BE.ReadModel.Shared.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Shared.Request
{
    public interface IGetOffersRequest : IRequest<IEnumerable<Offer>>
    {
        public object Filters { get; set; }
    }
}
