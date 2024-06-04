

using Entities.Models;
using Services;

namespace firstWebApi.middlewares
{
    public class RatingMiddleware
    {
        private IRatingService _ratingService;
        private readonly RequestDelegate _next;
        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            _ratingService = ratingService;
            Rating rating = new Rating();
            rating.Host = httpContext.Request.Host.ToString();
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path;
            rating.Referer = httpContext.Request.Host + httpContext.Request.Protocol;
            rating.UserAgent = httpContext.Request.Headers["User-Agent"];
            rating.RecordDate = DateTime.Now;
            await _ratingService.AddRating(rating);
            await _next(httpContext);
        }
    }
    public static class RatingMiddlewareExtentions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}

