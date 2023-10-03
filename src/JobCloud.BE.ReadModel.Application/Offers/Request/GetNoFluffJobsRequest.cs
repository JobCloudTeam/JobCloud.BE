using JobCloud.BE.ReadModel.Core.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Application.Request
{
    public class GetNoFluffJobsRequest : IRequest<IEnumerable<Offer>>
    {
        public object Filters { get; set; }
    }
}
