using Microsoft.Playwright;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace ScrapperTesting;
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
        var location = "/all-locations/";
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
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
        {
            Headless = true,
        });

        ParallelOptions options = new()
        {
            MaxDegreeOfParallelism = 4,
        };
        await Parallel.ForEachAsync(technologies, options, async (tech, token) =>
        {
            Console.WriteLine($"Starts task for: {tech}");
            var elements = await browser.GetTechnologyOffers(baseUrl, location, $"/{tech}");
            Console.WriteLine($"tech: {tech}, elements count: {elements.Count}");
        });
    }

    private async static Task<Dictionary<int, string>> GetTechnologyOffers(this IBrowser browser, string baseUrl, string location, string technology)
    {
        var allElements = new Dictionary<int, string>();
        int addedCount = 0;
        int retryCount = 0;


        var watch = System.Diagnostics.Stopwatch.StartNew();
        var page = await browser.NewPageAsync();
        await page.GotoAsync($"{baseUrl}{location}{technology}");
        watch.Stop();


        var elapsedS = watch.ElapsedMilliseconds / 1000f;
        Console.WriteLine($"Task Elapsed seconds: {elapsedS}");
        try
        {
            while (true)
            {
                await page.WaitForSelectorAsync("#__next > div.MuiBox-root.css-1v89lmg > div.css-c4vap3 > div > div.MuiBox-root.css-1fmajlu > div > div > div[data-virtuoso-scroller='true'] > div > div[data-test-id='virtuoso-item-list'] > div");
                var currentChunkElements = await page.QuerySelectorAllAsync("#__next > div.MuiBox-root.css-1v89lmg > div.css-c4vap3 > div > div.MuiBox-root.css-1fmajlu > div > div > div[data-virtuoso-scroller='true'] > div > div[data-test-id='virtuoso-item-list'] > div");
                foreach (var element in currentChunkElements)
                {
                    try
                    {
                        var hrefEl = await element.QuerySelectorAsync("a");
                        var href = await hrefEl.GetAttributeAsync("href");

                        var elementIndex = int.Parse(await element.GetAttributeAsync("data-item-index"));
                        if (!allElements.ContainsKey(elementIndex))
                        {
                            allElements.Add(elementIndex, href);
                            addedCount++;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}\n, skipping offer");
                        continue;
                    }
                }
                if (addedCount == 0)
                {
                    retryCount++;
                }
                else
                {
                    retryCount = 0;
                }
                if (retryCount == 3)
                {
                    break;
                }
                addedCount = 0;
                await page.Mouse.WheelAsync(0, 750);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}\n, cos sie srogo spierdoliło");
        }

        return allElements;
    }
    private static async Task<List<List<string>>> ChungBy(this List<string> array, int chunksCount)
    {
        double size = (double)array.Count / (double)chunksCount;
        size = Math.Round(size);

        var chunks = new List<List<string>>();

        for (int i = 0; i < chunksCount; i++)
        {
            var chunk = new List<string>();
            for (int j = i * (int)size, k = 0; k < size; j++, k++)
            {
                try
                {
                    chunk.Add(array[j]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }
            chunks.Add(chunk);
        }
        return chunks;
    }
}