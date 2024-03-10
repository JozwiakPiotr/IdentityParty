using IdentityParty.Core.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityParty.Core.Extensions;

public static class RouteBuilderExtensions
{
    //TODO: MapGroup is not available on .NET6, add support for .NET6
    public static RouteGroupBuilder MapIdentityParty(this IEndpointRouteBuilder builder)
    {
        var sp = builder.ServiceProvider;
        var group = builder.MapGroup("/");

        foreach (var endpoint in sp.GetServices<IEndpoint>()) endpoint.Map(group);

        return group;
    }
}