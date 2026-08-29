namespace ProjectApi.MiddleWare
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleWare>();
        }
    }
}