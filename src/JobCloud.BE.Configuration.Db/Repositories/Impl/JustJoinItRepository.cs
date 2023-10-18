using Dapper;
using JobCloud.BE.Configuration.Db.Factories;
using JobCloud.BE.Configuration.Db.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace JobCloud.BE.Configuration.Db.Repositories.Impl
{
    public class JustJoinItRepository : IJustJoinItRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<JustJoinItRepository> _logger;

        public JustJoinItRepository(IDbConnectionFactory dbConnectionFactory,
            ILogger<JustJoinItRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<TechnologyLink>> GetTechnologyLinks()
        {
            using (var connection = _dbConnectionFactory.Connection)
            {
                var technologies = await connection.QueryAsync<TechnologyLink>(
                    Sql.Queries.GetTechnologyLinks,
                    commandType: CommandType.Text);

                return technologies;
            }
        }

        public async Task<bool> UpdateTechnologyLinks(IEnumerable<TechnologyLink> technologyLinks)
        {
            try
            {
                using (var connection = _dbConnectionFactory.Connection)
                {
                    await connection.ExecuteAsync(Sql.Queries.UpdateTechnologyLinks,
                        technologyLinks.Select(x => new
                        {
                            Technology = x.Technology.ToString(),
                            Link = x.Link
                        }));
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[JobCloud][Configuration] request: {source} error: {error} stackTrace: {stackTrace}",
                    nameof(UpdateTechnologyLinks),
                    ex.Message,
                    ex.StackTrace);
                return false;
            }
        }

        public async Task<IEnumerable<DivName>> GetDivNames()
        {
            using (var connection = _dbConnectionFactory.Connection)
            {
                var divNames = await connection.QueryAsync<DivName>(Sql.Queries.GetDivNames,
                    commandType: CommandType.Text);

                return divNames;
            }
        }

        public async Task<bool> UpdateDivNames(IEnumerable<DivName> divNames)
        {
            try
            {
                using (var connection = _dbConnectionFactory.Connection)
                {
                    await connection.ExecuteAsync(Sql.Queries.UpdateDivNames,
                        divNames.Select(x => new
                        {
                            Div = x.Div.ToString(),
                            Name = x.Name
                        }));
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[JobCloud][Configuration] request: {source} error: {error} stackTrace: {stackTrace}",
                    nameof(UpdateTechnologyLinks),
                    ex.Message,
                    ex.StackTrace);
                return false;
            }
        }
    }
}
