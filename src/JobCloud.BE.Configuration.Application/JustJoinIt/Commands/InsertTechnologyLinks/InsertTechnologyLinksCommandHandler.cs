using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks;
using JobCloud.BE.Configuration.Db.Repositories;
using JobCloud.BE.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertTechnologyLinks
{
    public class InsertTechnologyLinksCommandHandler : IRequestHandler<InsertTechnologyLinksCommand, InsertTechnologyLinksCommandResponse>
    {
        private readonly ILogger<InsertTechnologyLinksCommandHandler> _logger;
        private readonly IJustJoinItRepository _repository;

        public InsertTechnologyLinksCommandHandler(ILogger<InsertTechnologyLinksCommandHandler> logger, IJustJoinItRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<InsertTechnologyLinksCommandResponse> Handle(InsertTechnologyLinksCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[JobCloud][Configuration] Start request {source}",
                nameof(GetTechnologylinksQueryHandler));

            var result = new InsertTechnologyLinksCommandResponse();

            var validatedLinks = await Validate(request);

            if (validatedLinks != null)
            {
                var status = await _repository.UpdateTechnologyLinks(validatedLinks.Select(x => x.Parse()));

                result.Status = status == true ? "Success" : "Failed";
            }

            return result;
        }

        private async Task<IEnumerable<TechnologyLinkDto>> Validate(InsertTechnologyLinksCommand request)
        {
            var technologiesCore = Enum.GetNames<Technology>();

            return request.TechnologyLinks.Where(x => technologiesCore.Any(y => y.Equals(x.Technology)));
        }
    }
}
