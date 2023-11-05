using Microsoft.AspNetCore.Mvc.Filters;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetUserId();
        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
        var user = await repo.UserRepository.GetByIdAsync(userId);
        user.LastActive = DateTime.UtcNow;
        await repo.Complete();
    }
}
