using final_work_x.BLL.Services;

namespace final_work_x.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Request

                await _next(context);

                // Response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                ServiceResponse response = ServiceResponse.Error(ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
