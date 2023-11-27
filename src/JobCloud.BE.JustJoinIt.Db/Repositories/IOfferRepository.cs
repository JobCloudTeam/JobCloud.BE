using JobCloud.BE.JustJoinIt.Core.Models;

namespace JobCloud.BE.JustJoinIt.Db.Repositories
{
    public interface IOfferRepository
    {
        Task InsertOffers(IEnumerable<Offer> offers);
    }
}
