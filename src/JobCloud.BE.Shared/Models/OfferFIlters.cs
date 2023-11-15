namespace JobCloud.BE.Shared.Models
{
    public class OfferFIlters
    {
        public string? Text { get; set; } = null;
        public decimal? MinSalary  { get; set; } = null;
        public decimal? MaxSalary  { get; set; } = null;
        public IEnumerable<string> Technology { get; set; } = Enumerable.Empty<string>();
    }
}
