using FluentValidation;
using Shortly.Application.ShortUrls.CustomeValidators;

namespace Shortly.Application.ShortUrls.CQRS.Commands.Add
{
    public class AddShortUrlCommandValidator
        : AbstractValidator<AddShortUrlCommand>
    {
        public AddShortUrlCommandValidator()
        {
            RuleFor(asuc => asuc.OriginalUrl)
                .ShouldBeValidUrl();
        }
    }
}