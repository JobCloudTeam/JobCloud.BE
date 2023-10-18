using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks;
using JobCloud.BE.Configuration.Db.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetDivNames
{
    public class GetDivNamesQueryHandler : IRequestHandler<GetDivNamesQuery, GetDivNamesQueryResponse>
    {
        private readonly IJustJoinItRepository _repository;
        private readonly ILogger<GetDivNamesQueryHandler> _logger;

        public GetDivNamesQueryHandler(IJustJoinItRepository repository,
            ILogger<GetDivNamesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetDivNamesQueryResponse> Handle(GetDivNamesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[JobCloud][Configuration] Start request {source}",
                 nameof(GetDivNamesQueryHandler));

            try
            {
                var divNames = await _repository.GetDivNames();
                return new GetDivNamesQueryResponse
                {
                    DivNames = divNames.Select(x => x.Parse())
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[JobCloud][Configuration] request: {source} error: {error} stackTrace: {stackTrace}",
                    nameof(GetDivNamesQueryHandler),
                    ex.Message,
                    ex.StackTrace);
                throw ex;
            }
        }
    }
}
