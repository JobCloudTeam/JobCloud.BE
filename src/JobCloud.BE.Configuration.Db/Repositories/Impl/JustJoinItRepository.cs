using Dapper;
using JobCloud.BE.Configuration.Db.Factories;
using JobCloud.BE.Configuration.Db.Models;
using System.Data;

namespace JobCloud.BE.Configuration.Db.Repositories.Impl
{
    public class JustJoinItRepository : IJustJoinItRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public JustJoinItRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
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
    }
}
