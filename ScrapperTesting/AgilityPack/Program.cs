using HtmlAgilityPack;
using System.Collections.Concurrent;
using System.Text;

namespace AgilityPack;
public static class Program
{
    public static async Task Main()
    {
        var baseUrl = "https://justjoin.it";
        Console.WriteLine("START");
        var watch = System.Diagnostics.Stopwatch.StartNew();
        var links = await GetAllLinks(baseUrl);
        var offers = await ScrapOffersFromLinks(links);
        watch.Stop();
        var elapsedS = watch.ElapsedMilliseconds / 1000f;
        Console.WriteLine($"Elapsed seconds: {elapsedS}");
        Console.WriteLine("--------------");
    }

    private async static Task<IEnumerable<string>> GetAllLinks(string baseUrl)
    {
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
            Console.WriteLine("adding links");
            result.Values.ToList().ForEach(x => links.Add(x));
        });

        return links;

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
                                    allElements.Add(elementIndex, baseUrl + href);
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

    private static async Task<IEnumerable<Offer>> ScrapOffersFromLinks(IEnumerable<string> links)
    {
        var offers = new ConcurrentBag<Offer>();
        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = 8,
        };
        await Parallel.ForEachAsync(links, options, async (link, token) =>
        {
            var result = await ScrapOffer(link);
            Console.WriteLine("adding offer");
            offers.Add(result);
        });

        return offers.ToList();
    }

    private static async Task<Offer> ScrapOffer(string url)
    {
        var web = new HtmlWeb();

        var doc = web.Load(url);
        var name = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[1]/span").InnerText;
        var company = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[2]/div[1]/div[2]/div[2]/div/div[1]").InnerText;

        var salary1Value = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[2]/div[1]/div[2]/div[2]/div[2]/div[1]/div[1]/div/span[1]");
        var salary1Type = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[2]/div[1]/div[2]/div[2]/div[2]/div[1]/div[1]/div/span[2]");
        var salary2Value = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[2]/div[1]/div[2]/div[2]/div[2]/div[1]/div[2]/div/span[1]");
        var salary2Type = doc.DocumentNode.SelectSingleNode("//*[@id=\"S:0\"]/div/div[2]/div[2]/div[1]/div[2]/div[2]/div[2]/div[1]/div[2]/div/span[2]");

        string salaryB2B = null;
        string salaryUOP = null;
        if (salary1Type is not null)
        {
            if (salary1Type.InnerText.Contains("B2B"))
            {
                salaryB2B = salary1Value.InnerText;
            }
            else
            {
                salaryUOP = salary1Value.InnerText;
            }
        }


        if (salary2Type is not null)
        {
            if (salary2Type.InnerText.Contains("B2B"))
            {
                salaryB2B = salary2Value.InnerText;
            }
            else
            {
                salaryUOP = salary2Value.InnerText;
            }
        }


        var techElements = doc.DocumentNode.SelectNodes("//div[@class=\"css-cjymd2\"]");

        var techStack = new List<string>();

        foreach (var tech in techElements)
        {
            var techname = tech.SelectSingleNode($"{tech.XPath}//h6").InnerText;

            if (techname != null)
            {
                techStack.Add(techname);
            }
        }

        var offer = new Offer
        {
            Name = name,
            CompanyName = company,
            SalaryUOP = salaryUOP,
            SalaryB2B = salaryB2B,
            TechStack = techStack
        };

        return offer;
    }

    public class Offer
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string SalaryUOP { get; set; }
        public string SalaryB2B { get; set; }
        public List<string> TechStack { get; set; }
    }
}