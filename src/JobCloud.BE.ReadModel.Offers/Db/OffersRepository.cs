using JobCloud.BE.ReadModel.Offers.Model;

namespace JobCloud.BE.ReadModel.Offers.Db
{
    public class OffersRepository : IOffersRepository
    {
        public OffersRepository()
        {
            // inject dapper
        }
        public async Task<IEnumerable<Offer>> GetJustJoinItOffers()
        {
            return GetMockedOffers();
        }

        public async Task<IEnumerable<Offer>> GetNoFluffJobsOffers()
        {
            return GetMockedOffers();
        }


        private IEnumerable<Offer> GetMockedOffers()
        {
            var random = new Random();
            return new List<Offer>()
            {
                new Offer{ Id = random.Next(0, 1000)},
                new Offer{ Id = random.Next(0, 1000)},
                new Offer{ Id = random.Next(0, 1000)},
                new Offer{ Id = random.Next(0, 1000)},
                new Offer{ Id = random.Next(0, 1000)},
                new Offer{ Id = random.Next(0, 1000)}
            };
        }
    }
}
