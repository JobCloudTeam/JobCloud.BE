using Dapper;
using JobCloud.BE.JustJoinIt.Core.Models;
using JobCloud.BE.JustJoinIt.Db.Factories;
using JobCloud.BE.JustJoinIt.Db.Sql;
using Microsoft.Extensions.Logging;
using System.Data;

namespace JobCloud.BE.JustJoinIt.Db.Repositories.Imp
{
    public class OfferRepository : IOfferRepository
    {
        private readonly DbContextFactory _dbContextFactory;
        private readonly ILogger<OfferRepository> _logger;

        public OfferRepository(DbContextFactory dbContextFactory, ILogger<OfferRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task InsertOffers(IEnumerable<Offer> offers)
        {
            try
            {
                using (var connection = _dbContextFactory.CreateConnection())
                {
                    var parameters = await GetInsertOffersParameters(offers);

                    await connection.ExecuteAsync(
                        Queries.InsertOffers,
                        parameters,
                        commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[JustJoinIt] {source} Error: {error}",
                    nameof(OfferRepository), ex.ToString());
                throw;
            }
        }

        private async Task<IEnumerable<object>> GetInsertOffersParameters(IEnumerable<Offer> offers)
        {
            var parameters = offers.ToList().Select(x => new
            {
                Name = x.Name,
                CompanyName = x.CompanyName,
                SalaryUOP = x.SalaryUOP,
                SalaryB2B = x.SalaryB2B,
                BaseTechnology = x.BaseTechnology,
                TechStack = string.Join("|", x.TechStack)
            });

            return parameters;
        }
    }
}
