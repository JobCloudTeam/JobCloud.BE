using JobCloud.BE.Configuration.WebApi.DTOs.JustJoinIt;
using JobCloud.BE.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JobCloud.BE.Configuration.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JustJoinItController : ControllerBase
    {
        private readonly ILogger<JustJoinItController> _logger;

        public JustJoinItController(ILogger<JustJoinItController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult SetLinksToTechnologies(SetLinkToTechnologyDTO queryParams)
        {
            var response = new List<Technology>();
            foreach (var item in queryParams.TechnologyLinks)
            {
                response.Add(TechnologyParser.Parse(item.Key));
            }

            return Ok(response);
        }

    }
}