using JobCloud.BE.Configuration.Application.DTOs;
using MediatR;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertTechnologyLinks
{
    public class InsertTechnologyLinksCommand : IRequest<InsertTechnologyLinksCommandResponse>
    {
        public IEnumerable<TechnologyLinkDto> TechnologyLinks { get; set; }
    }
}
