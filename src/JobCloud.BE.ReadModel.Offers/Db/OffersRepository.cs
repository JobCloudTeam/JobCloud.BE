using JobCloud.BE.ReadModel.Offers.Model;

namespace JobCloud.BE.ReadModel.Offers.Db
{
    public class OffersRepository : IOffersRepository
    {
        public OffersRepository()
        {
            // inject dapper
        }
        public Task<IEnumerable<Offer>> GetJustJoinItOffers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetNoFluffJobsOffers()
        {
            throw new NotImplementedException();
        }
    }
}
