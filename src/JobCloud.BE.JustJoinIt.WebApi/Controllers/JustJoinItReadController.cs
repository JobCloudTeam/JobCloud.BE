using JobCloud.BE.JustJoinIt.Application.Write.ScrapOffers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobCloud.BE.JustJoinIt.Host.Controllers
{
    public class JustJoinItReadController : ControllerBase
    {
        private readonly ISender _sender;

        public JustJoinItReadController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Route("/scrappOffers")]
        public async Task<IActionResult> ScrappOffers()
        {
            var response = await _sender.Send(new ScrapOffersRequest());
            return Ok(response);
        }
    }
}
