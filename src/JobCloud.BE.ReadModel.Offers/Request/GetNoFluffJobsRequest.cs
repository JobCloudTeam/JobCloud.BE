using JobCloud.BE.ReadModel.Offers.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Offers.Request
{
    public class GetNoFluffJobsRequest : IRequest<IEnumerable<Offer>>
    {
        public object Filters { get; set; }
    }
}
