using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Db.Repositories;
using JobCloud.BE.Shared.Enums.JustJoinIt;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertDivNames
{
    public class InsertDivNamesCommandHandler : IRequestHandler<InsertDivNamesCommand, InsertDivNamesCommandResponse>
    {
        private readonly ILogger<InsertDivNamesCommandHandler> _logger;
        private readonly IJustJoinItRepository _repository;

        public InsertDivNamesCommandHandler(ILogger<InsertDivNamesCommandHandler> logger, IJustJoinItRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<InsertDivNamesCommandResponse> Handle(InsertDivNamesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[JobCloud][Configuration] Start request {source}",
                nameof(InsertDivNamesCommandHandler));

            var result = new InsertDivNamesCommandResponse();

            var validatedDivs = await Validate(request);

            if (validatedDivs != null)
            {
                var status = await _repository.UpdateDivNames(validatedDivs.Select(x => x.Parse()));

                result.Status = status == true ? "Success" : "Failed";
            }

            return result;
        }

        private async Task<IEnumerable<DivNameDto>> Validate(InsertDivNamesCommand request)
        {
            var divsCore = Enum.GetNames<Div>();
            return request.DivNames.Where(x => !divsCore.Any(y => y.Equals(x.Div)));
        }
    }
}
