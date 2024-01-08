using JobCloud.BE.Domain.Models;
using JobCloud.BE.ReadModel.Application.Request;
using JobCloud.BE.ReadModel.Application.Services;
using JobCloud.BE.ReadModel.Core.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobCloud.BE.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IRequestsAggregator _requestsAggregator;
        private readonly ISender _sender;

        public OfferController(IRequestsAggregator requestsAggregator, ISender sender)
        {
            _requestsAggregator = requestsAggregator;
            _sender = sender;
        }


        [HttpGet]
        public async Task<IEnumerable<Offer>> GetOffers(Filters filters)
        {
            var jjit = new GetJustJoinItOffersRequest { Filters = filters };
            var requests = new List<IRequest<IEnumerable<Offer>>>
            {
                jjit
            };

            return await _requestsAggregator.AggregateJobOffers(requests, _sender);
        }

    }
}
