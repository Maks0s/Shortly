using ErrorOr;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Application.Services;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Domain.Entities;
using Shortly.Application.Common.AppErrors;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Add
{
    public class AddShortUrlCommandHandler
        : ICommandHandler<AddShortUrlCommand, ShortUrl?>
    {
        private readonly IUrlShorteningService _urlShorteningService;

        private readonly IShortUrlRepository _shortUrlRepository;

        public AddShortUrlCommandHandler(
                IShortUrlRepository shortUrlRepository,
                IUrlShorteningService urlShorteningService
            )
        {
            _shortUrlRepository = shortUrlRepository;
            _urlShorteningService = urlShorteningService;
        }

        public async Task<ErrorOr<ShortUrl?>> Handle(
                AddShortUrlCommand command,
                CancellationToken cancellationToken
            )
        {
            var shortenedUrlKey =
                await _urlShorteningService
                    .GenerateShortenedUrlKeyAsync();

            var urlToAdd = new ShortUrl()
            {
                ShortenedUrlKey = shortenedUrlKey,
                OriginalUrl = command.OriginalUrl,
                CreationDate = DateTime.UtcNow,
                TransitionCount = 0
            };

            var addedUrl =
                await _shortUrlRepository.AddShortUrlAsync(urlToAdd);

            if (addedUrl is null)
            {
                return Errors.ServerDataManipulation.NotAdded();
            }

            return addedUrl;
        }
    }
}