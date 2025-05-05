namespace Philomela.Api.Middlewares
{
    public class TokenMiddleware
    {
        public const string HEADERS_NAME = "refresh";
        public const string COOKIE_NAME = ".Auth.User.Id";
        public const string COOKIE_NAME_REFRESH = ".Auth.Ref";

        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/api/Authentication/refresh")
            {
                var tokenRefr = context.Request.Cookies[COOKIE_NAME_REFRESH];
                if (string.IsNullOrEmpty(tokenRefr) == false) {
                    context.Request.Headers.Append(HEADERS_NAME, tokenRefr);
                }
            }
            
            var token = context.Request.Cookies[COOKIE_NAME];
            if (string.IsNullOrEmpty(token) == false)
            {
                context.Request.Headers.Authorization = "Bearer " + token;
            }
            
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Append("X-Xss-Protection", "1");
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            await _next(context);
        }
    }
}
