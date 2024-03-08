using IdentityParty.Core.Abstractions;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityParty.Core.Endpoints.V1.Authorization;

internal sealed class AuthorizationEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("authorization", Handle);
    }

    public Task<IResult> Handle(HttpContext ctx, AuthorizationRequest request)
    {
        Results.Ok(new SuccessfulAuthorizationResponse());
        throw new NotImplementedException();
    }
}