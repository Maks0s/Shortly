using Riok.Mapperly.Abstractions;
using Shortly.Application.ShortUrls.CQRS.Commands.Add;
using Shortly.Domain.Entities;
using Shortly.Presentation.Common.DTOs.UrlDTOs;

namespace Shortly.Presentation.Common.Mappers
{
    [Mapper]
    public partial class UrlMapper
    {
        [MapProperty(nameof(UrlRequest.Url), nameof(AddShortUrlCommand.OriginalUrl))]
        public partial AddShortUrlCommand MapToAddShortUrlCommand(UrlRequest urlRequest);

        public partial UrlResponse MapToUrlResponse(ShortUrl shortUrl);
    }
}