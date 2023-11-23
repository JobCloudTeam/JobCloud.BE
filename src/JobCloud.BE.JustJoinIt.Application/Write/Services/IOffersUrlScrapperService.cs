using JobCloud.BE.JustJoinIt.Application.Models;

namespace JobCloud.BE.JustJoinIt.Application.Write.Services
{
    public interface IOfferUrlScrapperService
    {
        Task<IEnumerable<Offer>> GetAllOffers();
    }
}
