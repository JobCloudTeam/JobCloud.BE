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
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
    }
}
