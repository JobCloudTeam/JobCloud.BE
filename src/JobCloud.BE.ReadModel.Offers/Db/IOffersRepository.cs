using JobCloud.BE.ReadModel.Offers.Model;

namespace JobCloud.BE.ReadModel.Offers.Db
{
    public interface IOffersRepository
    {
        Task<IEnumerable<Offer>> GetJustJoinItOffers();
        Task<IEnumerable<Offer>> GetNoFluffJobsOffers();
    }
}
