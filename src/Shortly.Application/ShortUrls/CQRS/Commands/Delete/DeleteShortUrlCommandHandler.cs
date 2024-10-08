using ErrorOr;
using Shortly.Application.Common.AppErrors;
using Shortly.Application.Common.Interfaces.Application.CQRS;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Delete
{
    public class DeleteShortUrlCommandHandler
        : ICommandHandler<DeleteShortUrlCommand, ErrorOr.Deleted>
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public DeleteShortUrlCommandHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(
                DeleteShortUrlCommand command, 
                CancellationToken cancellationToken
            )
        {
            var deletedUrlsCount =
                await _shortUrlRepository.DeleteUrlAsync(command.ShortenedUrlKey);

            if (deletedUrlsCount < 1)
            {
                return Errors.Urls.NotFound();
            }

            return Result.Deleted;
        }
    }
}