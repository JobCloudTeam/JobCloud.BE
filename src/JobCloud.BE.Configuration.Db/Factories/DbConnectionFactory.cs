using System.Data;
using System.Data.SqlClient;

namespace JobCloud.BE.Configuration.Db.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString
                ?? throw new ArgumentNullException($"[JobCloud] [Configuration] argument cannot be null: {nameof(connectionString)}");
        }

        public IDbConnection Connection => new SqlConnection(_connectionString);
    }
}
