using JobCloud.BE.Configuration.Application.DTOs;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks
{
    public class GetTechnologyLinksQueryResponse
    {
        public IEnumerable<TechnologyLinkDto> TechnologyLinks { get; set; }
    }
}
