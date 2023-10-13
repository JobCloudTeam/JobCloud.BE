using JobCloud.BE.Configuration.Db.Models;

namespace JobCloud.BE.Configuration.Db.Repositories
{
    public interface IJustJoinItRepository
    {
        Task<IEnumerable<TechnologyLink>> GetTechnologyLinks();
    }
}
