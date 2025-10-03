#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAmazonQ.Services;

namespace TestAmazonQ.Attributes;

public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public RequirePermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var username = user.Identity.Name;
        var permissionService = context.HttpContext.RequestServices.GetRequiredService<PermissionService>();
        
        var hasPermission = await permissionService.UserHasPermissionAsync(username, _permission);
        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}