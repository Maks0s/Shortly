using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Update
{
    public record UpdateShortUrlCommand(
            string ShortenedUrlKey
        ) : ICommand<ShortUrl?>;
}