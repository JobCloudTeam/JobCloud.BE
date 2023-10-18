using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertDivNames;
using JobCloud.BE.Configuration.Application.JustJoinIt.Commands.InsertTechnologyLinks;
using JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetDivNames;
using JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks;
using JobCloud.BE.Configuration.Db.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobCloud.BE.Configuration.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JustJoinItController : ControllerBase
    {
        private readonly ILogger<JustJoinItController> _logger;
        private readonly IJustJoinItRepository _justJoinItRepository;
        private readonly ISender _sender;

        public JustJoinItController(
            ISender sender,
            IJustJoinItRepository justJoinItRepository,
            ILogger<JustJoinItController> logger)
        {
            _sender = sender;
            _logger = logger;
            _justJoinItRepository = justJoinItRepository;
        }

        [Route("currentLinks")]
        [HttpGet]
        public async Task<ActionResult<GetTechnologyLinksQueryResponse>> GetCurrentLinksToTechnologies()
        {
            var response = await _sender.Send(new GetTechnologyLinksQuery());
            return Ok(response);
        }

        [Route("insertLinks")]
        [HttpPost]
        public async Task<ActionResult<InsertTechnologyLinksCommandResponse>> SetLinksToTechnologies(IEnumerable<TechnologyLinkDto> queryParams)
        {
            var response = await _sender.Send(new InsertTechnologyLinksCommand { TechnologyLinks = queryParams });
            return Ok(response);
        }

        [Route("getDivNames")]
        [HttpGet]
        public async Task<ActionResult<GetDivNamesQueryResponse>> GetDivNames()
        {
            var response = await _sender.Send(new GetDivNamesQuery());
            return Ok(response);
        }


        [Route("insertDivNames")]
        [HttpPost]
        public async Task<ActionResult> SetDivNames(IEnumerable<DivNameDto> queryParams)
        {
            var response = await _sender.Send(new InsertDivNamesCommand { DivNames = queryParams });
            return Ok(response);
        }
    }
}