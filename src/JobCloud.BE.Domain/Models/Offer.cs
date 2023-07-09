namespace JobCloud.BE.Domain.Models
{
    public class Offer : DomainEntity
    {
        public string CompanyName { get; set; }
        public string Link { get; set; }
        public ICollection<Technology> Technologies { get; set; }
        public SalaryRange? SalaryUop { get; set; }
        public SalaryRange? SalaryB2B { get; set; }
    }
}
