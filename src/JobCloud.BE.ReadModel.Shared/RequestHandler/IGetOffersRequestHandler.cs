using JobCloud.BE.ReadModel.Shared.Model;
using JobCloud.BE.ReadModel.Shared.Request;
using MediatR;

namespace JobCloud.BE.ReadModel.Shared.RequestHandler
{
    public interface IGetOffersRequestHandler : IRequestHandler<IGetOffersRequest, IEnumerable<Offer>>
    {
        
    }
}
