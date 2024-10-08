using ErrorOr;
using System.Net;

namespace Shortly.Application.Common.AppErrors
{
    public static partial class Errors
    {
        public static class Urls
        {
            public static Error NotFound() =>
                Error.Custom(
                        (int)HttpStatusCode.NotFound,
                        "This short link does not have the original link",
                        "The short link doesn't lead anywhere. You may have made a typo, try again."
                    );
        }
    }
}