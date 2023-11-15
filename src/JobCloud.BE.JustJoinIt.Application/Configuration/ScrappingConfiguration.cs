namespace JobCloud.BE.JustJoinIt.Application.Configuration
{
    public static class ScrappingConfiguration
    {
        public static string BaseUrl { get; } = "https://justjoin.it/all-locations";
        public static IEnumerable<string> Technologies { get; } = new List<string>()
            {
                "javascript",
                "html",
                "php",
                "ruby",
                "python",
                "java",
                "net",
                "scala",
                "c",
                "mobile",
                "testing",
                "devops",
                "admin",
                "ux",
                "pm",
                "game",
                "analytics",
                "security",
                "data",
                "go",
                "support",
                "erp",
                "architecture",
            };
    }
}
