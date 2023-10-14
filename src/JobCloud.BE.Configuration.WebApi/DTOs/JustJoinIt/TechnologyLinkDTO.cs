using JobCloud.BE.Configuration.Db.Models;
using JobCloud.BE.Shared.Enums;

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

        public static TechnologyLink Parse(this TechnologyLinkDTO technologyLink)
        {
            return new TechnologyLink
            {
                Technology = TechnologyParser.Parse(technologyLink.Technology),
                Link = technologyLink.Link
            };
        }
    }
}
