using ErrorOr;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Queries.GetAll
{
    public class GetAllUrlsQueryHandler
        : IQueryHandler<GetAllUrlsQuery, List<ShortUrl>>
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public GetAllUrlsQueryHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ErrorOr<List<ShortUrl>>> Handle(
                GetAllUrlsQuery query, 
                CancellationToken cancellationToken
            )
        {
            var allUrls =
                await _shortUrlRepository.GetAllUrlsAsync();

            return ErrorOrFactory.From(allUrls);
        }
    }
}