using HtmlAgilityPack;
using System.Collections.Concurrent;

namespace AgilityPack;
public static class Program
{
    public static async Task Main()
    {
        Console.WriteLine("START");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        await GetAllLinks();
        watch.Stop();
        var elapsedS = watch.ElapsedMilliseconds / 1000f;
        Console.WriteLine($"Elapsed seconds: {elapsedS}");
        Console.WriteLine("--------------");
    }

    private async static Task GetAllLinks()
    {
        var baseUrl = "https://justjoin.it";
        var location = "all-locations/";
        var technologies = new List<string>
        {
            "java", //1
            "php",
            "mobile",
            "python",
            "data", //2
            "testing",
            "analytics",
            "scala",
            "javascript", //3
            "architecture",
            "net",
            "security",
            "admin", //4
            "support",
            "c",
            "ux",
            "devops", //5
            "ruby",
            "go",
            "html",
            "pm", //6
            "game",
            "erp",
        };

        var links = new ConcurrentBag<string>();
        var web = new HtmlWeb();

        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = 8,
        };
        await Parallel.ForEachAsync(technologies, options, async (tech, token) =>
        {
            var result = await GetTechnologyOffers(web, baseUrl, location, tech);
            result.Values.ToList().ForEach(x => links.Add(x));
        });
        Console.WriteLine($"Offers count: {links.Count}");
        Console.WriteLine("--------------");
    }

    private async static Task<Dictionary<int, string>> GetTechnologyOffers(HtmlWeb web, string baseUrl, string location, string technology)
    {
        var allElements = new Dictionary<int, string>();

        int index = 0;
        int addedCount = 0;
        do
        {
            addedCount = 0;
            var doc = web.Load($"{baseUrl}/{location}/{technology}?index={index}");
            var anchorElements = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div[2]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div");
            if (anchorElements != null)
            {
                foreach (var anchor in anchorElements)
                {
                    var elementIndex = anchor.GetAttributeValue<int>("data-item-index", -1);

                    if (elementIndex != -1)
                    {
                        var hrefEl = anchor.SelectSingleNode(".//a");
                        if (hrefEl != null)
                        {
                            string href = hrefEl.GetAttributeValue("href", "");
                            if (!string.IsNullOrEmpty(href))
                            {
                                if (!allElements.ContainsKey(elementIndex))
                                {
                                    allElements.Add(elementIndex, href);
                                    addedCount++;
                                }
                            }
                        }
                    }
                }
                index += addedCount;
            }

        } while (addedCount != 0);
        return allElements;
    }
}