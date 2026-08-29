namespace ProjectApi.MiddleWare
{
    public static class GlobalExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionMiddleWare>();
        }
    }
    public class GlobalExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleWare> _logger;
        public GlobalExceptionMiddleWare(RequestDelegate next,ILogger<GlobalExceptionMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new {status = 500,
                message = "An unexcepted error occured",
                detail = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? ex.Message : null};
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
    
}


// Bu middleware, pipeline'daki sonraki tüm adımları (_next) try-catch içine alarak 
// uygulamada herhangi bir yerde fırlatılan yakalanmamış hataları merkezi bir noktadan yakalar;
// hatayı loglar, response'u 500 Internal Server Error olarak ayarlar ve istemciye JSON formatında standart bir hata cevabı döner;
// hata detayı (ex.Message) yalnızca Development ortamında gösterilir, Production'da ise güvenlik amacıyla gizlenir, 
// böylece her controller'da ayrı ayrı try-catch yazmaya gerek kalmadan tutarlı ve güvenli bir hata yönetimi sağlanmış olur.