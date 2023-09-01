using SNJGlobalAPI.DtoModels;
using System.Text.Json;

namespace SNJGlobalAPI.GeneralServices
{
    public class EHandler : IMiddleware
    {
        private readonly ILogger<EHandler> _logger;
        public EHandler(ILogger<EHandler> logger)
        {
            _logger = logger;
            //_logger.LogInformation($"Using same at {DateTime.UtcNow}");
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.LogInformation($"*** Method {context.Request.Path.Value} Starts at {DateTime.Now} ***");
                await next(context);
                _logger.LogInformation($"*** Request Ends Successfully at {DateTime.Now} ***\n\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("\n*** EXCEPTION ***");
                _logger.LogError($"Error at {DateTime.Now} ##### {ex}", ex.Message);

                Responder<object> responder = new()
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };

                var json = JsonSerializer.Serialize(responder);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
                _logger.LogInformation($"*** EXCEPTION ENDS at {DateTime.Now} ***\n\n");
            }
        }
    }
}
