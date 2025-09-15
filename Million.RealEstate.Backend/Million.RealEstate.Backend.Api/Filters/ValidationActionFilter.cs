using Microsoft.AspNetCore.Mvc.Filters;
using Million.RealEstate.Backend.Application.Common.Exceptions;

namespace Million.RealEstate.Backend.Api.Filters;

public class ValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            throw new ValidationException(errors);
        }

        await next();
    }
}
