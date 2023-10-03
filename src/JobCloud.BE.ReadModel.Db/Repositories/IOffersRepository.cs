using JobCloud.BE.ReadModel.Core.Model;

namespace JobCloud.BE.ReadModel.Db.Repositories
{
    public interface IOffersRepository
    {
        Task<IEnumerable<Offer>> GetJustJoinItOffers();
        Task<IEnumerable<Offer>> GetNoFluffJobsOffers();
    }
}
