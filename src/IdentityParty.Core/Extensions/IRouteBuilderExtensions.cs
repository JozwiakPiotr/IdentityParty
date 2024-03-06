using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityParty.Core.Extensions;

public static class RouteBuilderExtensions
{
    public static void MapIdentityParty(this IRouteBuilder builder)
    {
        builder.MapGet("/tralala", (HttpContext ctx) => Task.CompletedTask);
    }
}