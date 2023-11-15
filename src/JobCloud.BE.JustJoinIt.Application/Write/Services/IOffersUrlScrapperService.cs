namespace JobCloud.BE.JustJoinIt.Application.Write.Services
{
    public interface IOfferUrlScrapperService
    {
        Task<IEnumerable<object>> GetAllOffers();
    }
}
