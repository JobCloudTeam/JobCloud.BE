using Dapper;
using JobCloud.BE.DB;
using JobCloud.BE.ReadModel.Offers.Model;

namespace JobCloud.BE.ReadModel.Offers.Db
{
    public class OffersRepository : IOffersRepository
    {
        private readonly DbContext _dbContext;

        public OffersRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Offer>> GetJustJoinItOffers()
        {
            if (false)  //TODO: CREATE DATABASE
            {
                using (var connection = _dbContext.CreateConnection())
                {
                    return await connection.QueryAsync<Offer>("some query");
                    //mapping
                }
            }
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
