using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shortly.Application.ShortUrls.CQRS.Queries.GetAll;
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
                    added => Created(
                            //TODO: Refactor to CreateAtAction
                            // after creating GetOriginalUrl
                            "mock",
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
    }
}