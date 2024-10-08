using ErrorOr;
using Shortly.Application.Common.AppErrors;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Application.ShortUrls.CQRS.Queries.GetById;

namespace Shortly.Application.ShortUrls.CQRS.Queries.GetOriginalUrl
{
    public class GetOriginalUrlQueryHandler
        : IQueryHandler<GetOriginalUrlQuery, string?>
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public GetOriginalUrlQueryHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ErrorOr<string?>> Handle(
                GetOriginalUrlQuery query, 
                CancellationToken cancellationToken
            )
        {
            var originalUrl =
                await _shortUrlRepository
                    .GetOriginalUrlAsync(query.ShortenedUrlKey);

            if (string.IsNullOrEmpty(originalUrl))
            {
                return Errors.Urls.NotFound();
            }

            return originalUrl;
        }
    }
}