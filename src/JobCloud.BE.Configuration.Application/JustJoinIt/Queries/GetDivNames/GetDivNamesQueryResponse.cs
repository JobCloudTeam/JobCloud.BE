using JobCloud.BE.Configuration.Application.DTOs;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetDivNames
{
    public class GetDivNamesQueryResponse
    {
        public IEnumerable<DivNameDto> DivNames { get; set; }
    }
}
