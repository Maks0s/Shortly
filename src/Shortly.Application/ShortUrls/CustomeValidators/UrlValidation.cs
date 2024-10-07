using FluentValidation;

namespace Shortly.Application.ShortUrls.CustomeValidators
{
    public static class UrlValidation
    {
        public static IRuleBuilderOptions<T, string> ShouldBeValidUrl<T>(
                this IRuleBuilderInitial<T, string> ruleBuilder
            )
        {
            return ruleBuilder
                .NotNull()
                .NotEmpty()
                .Must(IsValidUrl).WithMessage("Invalid URL format.");
        }

        private static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}