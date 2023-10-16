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
        }

        private async Task Validate(InsertTechnologyLinksCommand request)
        {
            var technologiesCore = Enum.GetNames<Technology>();
            foreach(var tech in request.TechnologyLinks.Select(x => x.Technology))
            {
                technologiesCore.Any(x => x == tech);
            }

            /*
             
            public class Program
{
	public static void Main()
	{
		List<int> list1 = new List<int>() {1,2,3,4,5};
		List<int> list2 = new List<int>() {1,2,8};
		
		var z = list2.Where(x => !list1.Any(y => y == x)).ToList();
		
		z.ForEach(x => Console.WriteLine(x));
	}
}

             */
        }
    }
}
