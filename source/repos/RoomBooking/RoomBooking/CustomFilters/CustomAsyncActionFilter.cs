using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class CustomAsyncActionFilter : IAsyncActionFilter
{
    private readonly ILogger<CustomAsyncActionFilter> _logger;

    public CustomAsyncActionFilter(ILogger<CustomAsyncActionFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("Executing action: " + context.ActionDescriptor.DisplayName);
        await next(); // Call the next middleware or action in the pipeline
        _logger.LogInformation("Executed action: " + context.ActionDescriptor.DisplayName);
    }
}
