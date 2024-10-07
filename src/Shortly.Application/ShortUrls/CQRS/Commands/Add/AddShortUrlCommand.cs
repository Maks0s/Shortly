using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Add
{
    public record AddShortUrlCommand(
            string OriginalUrl
        ) : ICommand<ShortUrl?>;
}