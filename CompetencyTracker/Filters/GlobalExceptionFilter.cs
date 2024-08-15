using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var result = new ObjectResult(new { error = "An error occurred while processing your request." })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        switch (context.Exception)
        {
            case InvalidCastException ex:
                result.StatusCode = StatusCodes.Status400BadRequest;
                break;
            case ArgumentException ex:
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Value = new { error = ex.Message };
                break;
            case OverflowException:
                result.StatusCode = StatusCodes.Status400BadRequest;
                result.Value = new { error = "Skill level is out of range for byte type (0-255)." };
                break;
            default:
                _logger.LogError(context.Exception, "An unhandled exception occurred.");
                break;
        }

        context.Result = result;
        context.ExceptionHandled = true;
    }
}