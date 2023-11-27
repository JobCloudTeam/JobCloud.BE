using JobCloud.BE.JustJoinIt.Core.Models;

namespace JobCloud.BE.JustJoinIt.Application.Write.Services
{
    public interface IOfferUrlScrapperService
    {
        Task<IEnumerable<Offer>> GetAllOffers();
    }
}
