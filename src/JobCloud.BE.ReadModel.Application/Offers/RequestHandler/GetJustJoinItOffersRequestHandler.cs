using JobCloud.BE.Domain.Models;
using JobCloud.BE.ReadModel.Application.Request;
using JobCloud.BE.ReadModel.Core.Model;
using JobCloud.BE.ReadModel.Db.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobCloud.BE.ReadModel.Application.RequestHandler
{
    public class GetJustJoinItOffersRequestHandler : IRequestHandler<GetJustJoinItOffersRequest, IEnumerable<Offer>>
    {
        private readonly IOffersRepository _repository;
        private readonly ILogger<GetJustJoinItOffersRequestHandler> _logger;

        public GetJustJoinItOffersRequestHandler(IOffersRepository repository,
            ILogger<GetJustJoinItOffersRequestHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Offer>> Handle(GetJustJoinItOffersRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[JobCloud] [ReadModel] [JustJoinIt] {nameof(GetJustJoinItOffersRequestHandler)} start");

            var offers = await _repository.GetJustJoinItOffers();

            var offersSelected = SelectOffers(offers.ToList(), request.Filters);

            return offers;
        }

        private async Task<IEnumerable<Offer>> SelectOffers(List<Offer> offers, Filters filters)
        {
            if (!String.IsNullOrEmpty(filters.Salary))
            {
                offers = offers.Where(x => x.SalaryB2B is not null && CheckSalary(filters.Salary, x.SalaryB2B)).ToList();
                offers = offers.Where(x => x.SalaryUOP is not null && CheckSalary(filters.Salary, x.SalaryUOP)).ToList();
            }

            if (!String.IsNullOrEmpty(filters.Technology))
            {
                offers = offers.Where(x => x.TechStack.Contains(filters.Technology) || x.BaseTechnology.Contains(filters.Technology)).ToList();
            }


            return offers;
        }

        private bool CheckSalary(string salaryFilter, string salaryOffer)
        {
            var minmaxOffer = salaryOffer.Replace(" ", "").Split('-').ToArray();
            var minmaxFilter = salaryFilter.Split('-').ToArray();

            if (int.Parse(minmaxFilter[0]) > int.Parse(minmaxOffer[0]))
            {
                return false;
            }

            if (int.Parse(minmaxFilter[1]) < int.Parse(minmaxOffer[1]))
            {
                return false;
            }

            return true;
        }
    }
}
