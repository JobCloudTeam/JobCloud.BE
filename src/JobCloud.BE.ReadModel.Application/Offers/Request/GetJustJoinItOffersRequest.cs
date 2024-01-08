using JobCloud.BE.Domain.Models;
using JobCloud.BE.ReadModel.Core.Model;
using MediatR;

namespace JobCloud.BE.ReadModel.Application.Request
{
    public class GetJustJoinItOffersRequest : IRequest<IEnumerable<Offer>>
    {
        public Filters Filters { get; set; }
    }
}
