#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAmazonQ.Services;

namespace TestAmazonQ.Attributes;

/// <summary>
/// Authorization attribute that requires specific permission
/// </summary>
public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public RequirePermissionAttribute(string permission)
    {
        _permission = permission;
    }

    /// <summary>
    /// Performs authorization check for required permission
    /// </summary>
    /// <param name="context">Authorization filter context</param>
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