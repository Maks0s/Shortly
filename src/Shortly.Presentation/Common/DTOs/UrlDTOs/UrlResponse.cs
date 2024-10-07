namespace Shortly.Presentation.Common.DTOs.UrlDTOs
{
    public record UrlResponse(
            string OriginalUrl,
            string ShortenedUrl,
            DateTime CreationDate,
            int TransitionCount
        );
}