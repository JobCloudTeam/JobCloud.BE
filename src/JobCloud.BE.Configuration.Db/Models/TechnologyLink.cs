using JobCloud.BE.Shared.Enums;

namespace JobCloud.BE.Configuration.Db.Models
{
    public class TechnologyLink
    {
        public Technology Technology { get; set; }
        public string Link { get; set; }
    }
}
