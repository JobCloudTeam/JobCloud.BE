using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JobCloud.BE.JustJoinIt.Db.Factories
{
    public class DbContextFactory
    {
        private readonly string _connectionString;

        public DbContextFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("JBCConnectionString");
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
