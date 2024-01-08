using HtmlAgilityPack;
using JobCloud.BE.JustJoinIt.Application.Configuration;
using JobCloud.BE.JustJoinIt.Core.Models;
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
        public async Task<IEnumerable<Offer>> GetAllOffers()
        {
            var urls = await GetUrls();

            var offers = await GetOffers(urls);

            return offers;
        }

        #region urls
        private async Task<IEnumerable<OfferUrl>> GetUrls()
        {
            var technologies = ScrappingConfiguration.Technologies;
            string location = "all-locations";
            var urls = new ConcurrentBag<OfferUrl>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 6,
            };

            await Parallel.ForEachAsync(technologies, options, async (tech, token) =>
            {
                var result = await GetTechnologyOfferUrls(tech, location);
                result.Values.ToList().ForEach(x => urls.Add(x));
            });

            return urls;
        }

        private async Task<Dictionary<int, OfferUrl>> GetTechnologyOfferUrls(string technology, string location)
        {
            var allElements = new Dictionary<int, OfferUrl>();
            var baseUrl = ScrappingConfiguration.BaseUrl;

            int index = 0;
            int addedCount = 0;
            do
            {
                addedCount = 0;
                var doc = _htmlWeb.Load($"{baseUrl}/{location}/{technology}?index={index}");
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
        #endregion

        #region offers

        private async Task<IEnumerable<Offer>> GetOffers(IEnumerable<OfferUrl> urls)
        {
            var offers = new ConcurrentBag<Offer>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 6,
            };

            await Parallel.ForEachAsync(urls, options, async (url, token) =>
            {
                try
                {
                    var result = await ScrapOffer(url);
                    offers.Add(result);
                }
                catch (Exception ex)
                {
                    // log(ex);
                }
            });


            return offers;
        }

        private async Task<Offer> ScrapOffer(OfferUrl url)
        {
            var doc = _htmlWeb.Load(url.Url);
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
                BaseTechnology = url.BaseTechnology,
                SalaryUOP = salaryUOP,
                SalaryB2B = salaryB2B,
                TechStack = techStack
            };

            return offer;
        }

        #endregion
    }
}
