using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.ExceptionHandler
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ApiExceptionHandler> _logger;

        public ApiExceptionHandler(ILogger<ApiExceptionHandler> logger)
        {
            _logger = logger;
        }


        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred.");

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "A internal server error occurred.",
                Detail = GetCustomErrorMessage(exception),
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

            return true;
        }

        private string GetCustomErrorMessage(Exception exception)
        {
            if (_customErrorMessages.TryGetValue(exception.GetType(), out var message))
            {
                return message;
            }

            return $"Ocorreu um erro inesperado: {exception.Message}";
        }

        private static readonly Dictionary<Type, string> _customErrorMessages = new()
        {
            { typeof(ArgumentNullException), "Um argumento nulo foi passado." },
            { typeof(InvalidOperationException), "Um argumento nulo foi passado." },
            { typeof(FileNotFoundException), "Um argumento nulo foi passado." }
        };


    }

}
