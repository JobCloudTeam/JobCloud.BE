using JobCloud.BE.JustJoinIt.Application.Models;

namespace JobCloud.BE.JustJoinIt.Application.Write.ScrapOffers
{
    public class ScrapOffersResponse
    {
        public string Status { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public IEnumerable<Offer> Offers { get; set; } //TODO: Remove from response
    }
}
