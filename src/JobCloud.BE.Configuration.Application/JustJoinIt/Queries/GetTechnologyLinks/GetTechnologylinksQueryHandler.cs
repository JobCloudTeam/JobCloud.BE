using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Db.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks
{
    public class GetTechnologylinksQueryHandler : IRequestHandler<GetTechnologyLinksQuery, GetTechnologyLinksQueryResponse>
    {
        private readonly ILogger<GetTechnologylinksQueryHandler> _logger;
        private readonly IJustJoinItRepository _repository;

        public GetTechnologylinksQueryHandler(ILogger<GetTechnologylinksQueryHandler> logger, IJustJoinItRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<GetTechnologyLinksQueryResponse> Handle(GetTechnologyLinksQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[JobCloud][Configuration] Start request {source}",
                nameof(GetTechnologylinksQueryHandler));

            try
            {
                var technologies = await _repository.GetTechnologyLinks();
                return new GetTechnologyLinksQueryResponse
                {
                    TechnologyLinks = technologies.Select(x => x.Parse())
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[JobCloud][Configuration] request: {source} error: {error} stackTrace: {stackTrace}",
                    nameof(GetTechnologylinksQueryHandler),
                    ex.Message,
                    ex.StackTrace);
                throw ex;
            }
        }
    }
}
