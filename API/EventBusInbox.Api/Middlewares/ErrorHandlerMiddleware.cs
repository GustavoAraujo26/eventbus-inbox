using EventBusInbox.Shared.Extensions;
using EventBusInbox.Shared.Models;
using Newtonsoft.Json;

namespace EventBusInbox.Api.Middlewares
{
    /// <summary>
    /// Middleware para manipulação de erros da aplicação
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger logger;

        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="next">Requisição</param>
        /// <param name="logger">Logger</param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            logger = LoggingExtensions.CreateAppLogger();
        }

        /// <summary>
        /// Método para acionar o manipulador
        /// </summary>
        /// <param name="context">Contexto da aplicação</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var appResponse = AppResponse<object>.Error(ex);

                response.StatusCode = (int)appResponse.Status;

                var result = JsonConvert.SerializeObject(appResponse);
                await response.WriteAsync(result);

                logger.Error(ex, "An error ocurred when execute the request!");
            }
        }
    }
}
