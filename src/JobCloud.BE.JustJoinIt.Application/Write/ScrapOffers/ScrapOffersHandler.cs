using JobCloud.BE.JustJoinIt.Application.Write.Services;
using JobCloud.BE.JustJoinIt.Core.Models;
using JobCloud.BE.JustJoinIt.Db.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace JobCloud.BE.JustJoinIt.Application.Write.ScrapOffers
{
    public class ScrapOffersHandler : IRequestHandler<ScrapOffersRequest, ScrapOffersResponse>
    {
        private readonly IOfferUrlScrapperService _offerUrlScrapperService;
        private readonly ILogger<ScrapOffersHandler> _logger;
        private readonly IOfferRepository _offerRepository;

        public ScrapOffersHandler(IOfferUrlScrapperService offerUrlScrapperService, ILogger<ScrapOffersHandler> logger, IOfferRepository offerRepository)
        {
            _offerUrlScrapperService = offerUrlScrapperService;
            _logger = logger;
            _offerRepository = offerRepository;
        }

        public async Task<ScrapOffersResponse> Handle(ScrapOffersRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[JustJoinIt] {source} Start processing request",
                nameof(ScrapOffersHandler));

            var response = new ScrapOffersResponse();
            try
            {
                var watch = Stopwatch.StartNew();
                var offers = await _offerUrlScrapperService.GetAllOffers();
                watch.Stop();

                response.ExecutionTime = watch.Elapsed;
                response.Status = "Success";
                response.Offers = offers;
                //TODO: change active offers status to false

                await _offerRepository.InsertOffers(offers);
            }
            catch (Exception ex)
            {
                _logger.LogError("[JustJoinIt] {source} Error: {error}",
                    nameof(ScrapOffersHandler), ex.ToString());

                response.Status = "Failed";
            }

            _logger.LogInformation("[JustJoinIt] {source} End processing request",
                nameof(ScrapOffersHandler));

            return response;
        }
    }
}