using ErrorOr;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Queries.GetAll
{
    public class GetAllQueryHandler
        : IQueryHandler<GetAllQuery, List<ShortUrl>>
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public GetAllQueryHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ErrorOr<List<ShortUrl>>> Handle(
                GetAllQuery query, 
                CancellationToken cancellationToken
            )
        {
            var allUrls =
                await _shortUrlRepository.GetAllUrlsAsync();

            return ErrorOrFactory.From(allUrls);
        }
    }
}