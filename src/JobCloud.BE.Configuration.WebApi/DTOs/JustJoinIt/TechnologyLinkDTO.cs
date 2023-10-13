using JobCloud.BE.Configuration.Db.Models;

namespace JobCloud.BE.Configuration.WebApi.DTOs.JustJoinIt
{
    public class TechnologyLinkDTO
    {
        public string Technology { get; set; }
        public string Link { get; set; }
    }

    public static class TechnologyLinkParser
    {
        public static TechnologyLinkDTO Parse(this TechnologyLink technologyLink)
        {
            return new TechnologyLinkDTO
            {
                Technology = technologyLink.Technology.ToString(),
                Link = technologyLink.Link
            };
        }
    }
}
