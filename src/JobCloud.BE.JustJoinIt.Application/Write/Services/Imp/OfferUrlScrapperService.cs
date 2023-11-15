using HtmlAgilityPack;
using JobCloud.BE.JustJoinIt.Application.Configuration;
using JobCloud.BE.Shared.Models;
using System.Collections.Concurrent;

namespace JobCloud.BE.JustJoinIt.Application.Write.Services.Imp
{
    public class OfferUrlScrapperService : IOfferUrlScrapperService
    {
        private readonly HtmlWeb _htmlWeb;
        public OfferUrlScrapperService()
        {
            _htmlWeb = new HtmlWeb();
        }
        public async Task<IEnumerable<object>> GetAllOffers()
        {
            var urls = await GetUrls();

            throw new NotImplementedException();
        }

        private async Task<IEnumerable<OfferUrl>> GetUrls()
        {
            var technologies = ScrappingConfiguration.Technologies;

            var urls = new ConcurrentBag<OfferUrl>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 6,
            };

            await Parallel.ForEachAsync(technologies, options, async (tech, token) =>
            {
                var result = await GetTechnologyOffers(tech);
                result.Values.ToList().ForEach(x => urls.Add(x));
            });

            return urls;
        }

        private async Task<Dictionary<int, OfferUrl>> GetTechnologyOffers(string technology)
        {
            var allElements = new Dictionary<int, OfferUrl>();
            var baseUrl = ScrappingConfiguration.BaseUrl;

            int index = 0;
            int addedCount = 0;
            do
            {
                addedCount = 0;
                var doc = _htmlWeb.Load($"{baseUrl}/{technology}?index={index}");
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
                                        allElements.Add(elementIndex, new OfferUrl
                                        {
                                            Url = baseUrl + href,
                                            BaseTechnology = technology
                                        });
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
}
