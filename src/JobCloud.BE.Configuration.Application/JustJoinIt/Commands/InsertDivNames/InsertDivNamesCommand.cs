using JobCloud.BE.Configuration.Application.DTOs;
using MediatR;

namespace JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertDivNames
{
    public class InsertDivNamesCommand : IRequest<InsertDivNamesCommandResponse>
    {
        public IEnumerable<DivNameDto> DivNames { get; set; }
    }
}
