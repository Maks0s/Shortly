using Shortly.Application.Common.Interfaces.Application.CQRS;

namespace Shortly.Application.ShortUrls.CQRS.Queries.GetById
{
    public record GetOriginalUrlQuery(
            string ShortenedUrlKey
        ) : IQuery<string?>;
}