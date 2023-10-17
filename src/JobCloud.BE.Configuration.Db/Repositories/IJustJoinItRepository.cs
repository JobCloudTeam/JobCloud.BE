using JobCloud.BE.Configuration.Db.Models;

namespace JobCloud.BE.Configuration.Db.Repositories
{
    public interface IJustJoinItRepository
    {
        Task<IEnumerable<TechnologyLink>> GetTechnologyLinks();

        Task<bool> UpdateTechnologyLinks(IEnumerable<TechnologyLink> technologyLinks);

        Task<IEnumerable<DivName>> GetDivNames();
    }
}
