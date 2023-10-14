using JobCloud.BE.Configuration.Db.Repositories;
using JobCloud.BE.Configuration.Db.Repositories.Impl;
using JobCloud.BE.Configuration.WebApi.DTOs.JustJoinIt;
using Microsoft.AspNetCore.Mvc;

namespace JobCloud.BE.Configuration.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JustJoinItController : ControllerBase
    {
        private readonly ILogger<JustJoinItController> _logger;
        private readonly IJustJoinItRepository _justJoinItRepository;

        public JustJoinItController(ILogger<JustJoinItController> logger, IJustJoinItRepository justJoinItRepository)
        {
            _logger = logger;
            _justJoinItRepository = justJoinItRepository;
        }

        [Route("currentLinks")]
        [HttpGet]
        public async Task<ActionResult<TechnologyLinkDTO>> GetCurrentLinksToTechnologies()
        {
            var technologyLinks = await _justJoinItRepository.GetTechnologyLinks();
            return Ok(technologyLinks.Select(x => x.Parse()));
        }

        [Route("insertLinks")]
        [HttpPost]
        public async Task<ActionResult<bool>> SetLinksToTechnologies(IEnumerable<TechnologyLinkDTO> queryParams)
        {
            var technologies = queryParams.Select(x => x.Parse());
            return await _justJoinItRepository.UpdateTechnologyLinks(technologies);
        }

    }
}