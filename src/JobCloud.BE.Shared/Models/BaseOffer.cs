namespace JobCloud.BE.Shared.Models
{
    public class BaseOffer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string SalaryUop { get; set; } //TODO: range value (will be used in filters)
        public string SalaryB2B { get; set; } //TODO: range value (will be used in filters)
        public string Technology { get; set; } //TODO: enum value (will be used in filters)
        public IEnumerable<string> TechStack { get; set; } //TODO: enum value (will be used in filters)

    }
}
