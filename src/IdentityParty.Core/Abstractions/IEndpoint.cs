using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityParty.Core.Abstractions;

internal interface IEndpoint
{
    void Map(IEndpointRouteBuilder builder);
}