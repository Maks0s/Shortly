using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Domain.Entities;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Delete
{
    public record DeleteShortUrlCommand(
            string ShortenedUrlKey
        ) : ICommand<ErrorOr.Deleted>;
}