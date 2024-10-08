using ErrorOr;
using Microsoft.Extensions.Configuration;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Application.Services;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Domain.Entities;
using Shortly.Application.Common.AppErrors;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Update
{
    public class UpdateShortUrlCommandHandler
        : ICommandHandler<UpdateShortUrlCommand, ShortUrl?>
    {
        private readonly IUrlShorteningService _urlShorteningService;

        private readonly IConfiguration _configuration;

        private readonly IShortUrlRepository _shortUrlRepository;

        public UpdateShortUrlCommandHandler(
                IShortUrlRepository shortUrlRepository,
                IUrlShorteningService urlShorteningService,
                IConfiguration configuration
            )
        {
            _shortUrlRepository = shortUrlRepository;
            _urlShorteningService = urlShorteningService;
            _configuration = configuration;
        }

        public async Task<ErrorOr<ShortUrl?>> Handle(
                UpdateShortUrlCommand command, 
                CancellationToken cancellationToken
            )
        {
            var originalUrl =
                await _shortUrlRepository
                    .GetOriginalUrlAsync(command.ShortenedUrlKey);

            if (string.IsNullOrEmpty(originalUrl))
            {
                return Errors.Urls.NotFound();
            }

            await _shortUrlRepository.DeleteUrlAsync(command.ShortenedUrlKey);

            var newShortenedUrlKey =
                await _urlShorteningService
                    .GenerateShortenedUrlKeyAsync();

            string shortenedUrl = $"{_configuration["BaseUrl"]}/{newShortenedUrlKey}";

            var newUrlToAdd = new ShortUrl()
            {
                ShortenedUrlKey = newShortenedUrlKey,
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
                CreationDate = DateTime.UtcNow,
                TransitionCount = 0
            };

            var updatedUrl =
                await _shortUrlRepository.AddShortUrlAsync(newUrlToAdd);

            if (updatedUrl is null)
            {
                return Errors.ServerDataManipulation.NotAdded();
            }

            return updatedUrl;
        }
    }
}