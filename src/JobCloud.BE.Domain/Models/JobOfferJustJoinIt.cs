
using JobCloud.BE.Shared.Enums;

namespace JobCloud.BE.Domain.Models
{
    public class JobOfferJustJoinIt
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public int CompanySize { get; set; }
        public string? CompanyLink { get; set; }
        public Level JobLevel { get; set; }
        public string CompanyLocation { get; set; }
        public string Link { get; set; }
        public IEnumerable<Technology> Technologies { get; set; } = Enumerable.Empty<Technology>();
        public SalaryRange? SalaryUop { get; set; }
        public SalaryRange? SalaryB2B { get; set; }
        public JobOfferSource Source { get; set; } = JobOfferSource.JustJoinIt;
    }
}
