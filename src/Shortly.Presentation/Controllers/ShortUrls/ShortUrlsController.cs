using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shortly.Application.ShortUrls.CQRS.Commands.Delete;
using Shortly.Application.ShortUrls.CQRS.Commands.Update;
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

        [HttpPut]
        [Route("{shortUrlKey}")]
        public async Task<ActionResult<UrlResponse>> UpdateShortUrl(string shortUrlKey)
        {
            var updateUrlCommand = new UpdateShortUrlCommand(shortUrlKey);

            var updateUrlResult = await _sender.Send(updateUrlCommand);

            return updateUrlResult.Match(
                    updated => Ok(
                            _mapper.MapToUrlResponse(updated)
                        ),
                    errors => Problem(errors)
                );
        }

        [HttpDelete]
        [Route("{shortUrlKey}")]
        public async Task<ActionResult> DeleteShortUrl(string shortUrlKey)
        {
            var deleteUrlCommand = new DeleteShortUrlCommand(shortUrlKey);

            var deleteUrlResult = await _sender.Send(deleteUrlCommand);

            return deleteUrlResult.Match(
                    success => NoContent(),
                    errors => Problem(errors)
                );
        }
    }
}