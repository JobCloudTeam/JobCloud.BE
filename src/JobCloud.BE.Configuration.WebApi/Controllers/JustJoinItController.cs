using JobCloud.BE.Configuration.Application.DTOs;
using JobCloud.BE.Configuration.Application.JustJoinIt.Queries.GetTechnologyLinks;
using JobCloud.BE.Configuration.Db.Repositories;
using JobCloud.BE.Configuration.Db.Repositories.Impl;
using JobCloud.BE.Configuration.WebApi.DTOs.JustJoinIt;
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
        public async Task<ActionResult<bool>> SetLinksToTechnologies(IEnumerable<TechnologyLinkDto> queryParams)
        {
            var technologies = queryParams.Select(x => x.Parse());
            return await _justJoinItRepository.UpdateTechnologyLinks(technologies);
        }

    }
}