namespace Shortly.Presentation.Common.DTOs.UrlDTOs
{
    public record UrlResponse(
            string ShortenedUrlKey,
            string OriginalUrl,
            string ShortenedUrl,
            DateTime CreationDate,
            int TransitionCount
        );
}