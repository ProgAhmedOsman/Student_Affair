using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Web;

namespace App.API.CustumeMiddleWare
{
    public class UrlEncodingMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlEncodingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalQueryString = context.Request.QueryString;
            var encodedQueryString = new QueryString(HttpUtility.UrlEncode(originalQueryString.Value));

            context.Request.QueryString = encodedQueryString;

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UrlEncodingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUrlEncodingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UrlEncodingMiddleware>();
        }
    }
}
