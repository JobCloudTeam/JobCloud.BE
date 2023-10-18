using JobCloud.BE.Configuration.Db.Models;
using JobCloud.BE.Shared.Enums;

namespace JobCloud.BE.Configuration.Application.DTOs
{
    public class TechnologyLinkDto
    {
        public string Technology { get; set; }
        public string Link { get; set; }
    }

    public static class TechnologyLinkParser
    {
        public static TechnologyLinkDto Parse(this TechnologyLink technologyLink)
        {
            return new TechnologyLinkDto
            {
                Technology = technologyLink.Technology.ToString(),
                Link = technologyLink.Link
            };
        }

        public static TechnologyLink Parse(this TechnologyLinkDto technologyLink)
        {
            return new TechnologyLink
            {
                Technology = TechnologyParser.Parse(technologyLink.Technology),
                Link = technologyLink.Link
            };
        }
    }
}
