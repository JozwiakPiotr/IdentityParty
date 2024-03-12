using Microsoft.AspNetCore.Http;

namespace IdentityParty.Core.Abstractions;

internal interface IEndpoint<in T>
{
    string HttpVerb { get; }
    string Path { get; }
    Task<IResult> HandleAsync(T Request);
}