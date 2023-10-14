using System.Data;

namespace JobCloud.BE.Configuration.Db.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}