using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.RequestHandler
{
    public interface IGetOffersRequestHandler : IRequestHandler<IGetOffersRequest, IEnumerable<Offer>>
    {
        
    }
}
