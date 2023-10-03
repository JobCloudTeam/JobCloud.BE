using JobCloud.BE.ReadModel.Application.Request;
using JobCloud.BE.ReadModel.Core.Model;
using JobCloud.BE.ReadModel.Db.Repositories;
using MediatR;

namespace JobCloud.BE.ReadModel.Application.RequestHandler
{
    public class GetNoFluffJobsRequestHandler : IRequestHandler<GetNoFluffJobsRequest, IEnumerable<Offer>>
    {
        private readonly IOffersRepository _repository;
        public GetNoFluffJobsRequestHandler(IOffersRepository repository) => _repository = repository;

        public async Task<IEnumerable<Offer>> Handle(GetNoFluffJobsRequest request, CancellationToken cancellationToken)
        {
            //loger

            //request.Filters

            //mapping justjoinit offer model to readmodel offer model

            return await _repository.GetNoFluffJobsOffers();
        }
    }
}
