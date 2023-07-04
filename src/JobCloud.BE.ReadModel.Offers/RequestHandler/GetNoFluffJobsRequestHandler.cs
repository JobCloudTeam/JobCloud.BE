using JobCloud.BE.ReadModel.Offers.Db;
using JobCloud.BE.ReadModel.Offers.Model;
using JobCloud.BE.ReadModel.Offers.Request;

namespace JobCloud.BE.ReadModel.Offers.RequestHandler
{
    public class GetNoFluffJobsRequestHandler
    {
        private readonly IOffersRepository _repository;
        public GetNoFluffJobsRequestHandler(IOffersRepository repository) => _repository = repository;

        public async Task<IEnumerable<Offer>> Handle(GetJustJoinItOffersRequest request, CancellationToken cancellationToken)
        {
            //loger

            //request.Filters

            //mapping justjoinit offer model to readmodel offer model

            return await _repository.GetNoFluffJobsOffers();
        }
    }
}
