﻿using Microsoft.AspNetCore.Mvc.Filters;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var username = resultContext.HttpContext.User.GetUsername();
        var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
        var user = await repo.GetByUserNameAsync(username);
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();
    }
}
