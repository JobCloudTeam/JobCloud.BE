using Dapper;
using JobCloud.BE.ReadModel.Core.Model;
using JobCloud.BE.ReadModel.Db.Factories;
using System.Data;

namespace JobCloud.BE.ReadModel.Db.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        private readonly DbContextFactory _dbContext;

        public OffersRepository(DbContextFactory dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Offer>> GetJustJoinItOffers()
        {
            using var connection = _dbContext.CreateConnection();

            var offers = await connection.QueryAsync<Offer>(
                Sql.Queries.GetJustJoinItOffers,
                commandType: CommandType.Text);

            return offers;
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
            };
        }
    }
}
