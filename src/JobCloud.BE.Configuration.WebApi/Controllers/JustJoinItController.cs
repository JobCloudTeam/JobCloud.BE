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

        [Route("technologies")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTechnologies()
        {
            var response = Enumerable.Empty<string>();
            return Ok(response);
        }


        [Route("technologies")]
        [HttpPost]
        public async Task<ActionResult> SetTechnologies()
        {
            return Ok();
        }

    }
}