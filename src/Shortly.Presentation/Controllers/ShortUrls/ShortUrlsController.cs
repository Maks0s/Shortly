using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shortly.Application.ShortUrls.CQRS.Queries.GetAll;
using Shortly.Application.ShortUrls.CQRS.Queries.GetById;
using Shortly.Presentation.Common.DTOs.UrlDTOs;
using Shortly.Presentation.Common.Mappers;
using Shortly.Presentation.Controllers.Common;

namespace Shortly.Presentation.Controllers.ShortUrls
{
    [Route("/")]
    public class ShortUrlsController : BaseApiController
    {
        private readonly ISender _sender;

        private readonly UrlMapper _mapper;

        public ShortUrlsController(ISender sender, UrlMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddShortUrl([FromBody] UrlRequest urlRequest)
        {
            var addCommand = _mapper.MapToAddShortUrlCommand(urlRequest);

            var addResult = await _sender.Send(addCommand);

            return addResult.Match(
                    added => CreatedAtAction(
                            nameof(GetOriginalUrl),
                            new { shortUrlKey = added.ShortenedUrlKey },
                            _mapper.MapToUrlResponse(added)
                        ),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        [Route("urls")]
        public async Task<ActionResult<List<UrlResponse>>> GetAllUrls()
        {
            var getAllQuery = new GetAllUrlsQuery();

            var getAllResult = await _sender.Send(getAllQuery);

            return getAllResult.Match(
                    received => Ok(
                        _mapper.MapToCollectionOfUrlResponses(received)
                    ),
                    errors => Problem(errors)
                );
        }

        [HttpGet]
        [Route("{shortUrlKey}")]
        public async Task<ActionResult> GetOriginalUrl(string shortUrlKey)
        {
            var getOriginalUrlQuery = new GetOriginalUrlQuery(shortUrlKey);

            var getOriginalUrlResult =
                await _sender.Send(getOriginalUrlQuery);

            return getOriginalUrlResult.Match(
                    url => Redirect(url),
                    errors => Problem(errors)
                );
        }
    }
}