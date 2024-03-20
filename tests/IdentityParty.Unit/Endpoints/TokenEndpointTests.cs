using AutoFixture;
using IdentityParty.Core;
using IdentityParty.Core.Endpoints.V1.Token;
using IdentityParty.Core.Endpoints.V1.Token.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IdentityParty.Unit;

public class TokenEndpointTests
{
    private Fixture _fixture;
    private TokenEndpoint _sut;

    public TokenEndpointTests()
    {
        _fixture = FixtureFactory.GetFixture();
    }

    public async Task HandleAsync_WhenRequestIsValid_ShouldReturnSuccessfulResponse()
    {
        var request = _fixture.Create<TokenRequest>();
        _sut = _fixture.Create<TokenEndpoint>();

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<Ok<SuccessfulTokenResponse>>(actual);
        var successfulResponse = (Ok<SuccessfulTokenResponse>)actual;
        Assert.NotNull(successfulResponse.Value);
        Assert.NotNull(successfulResponse.Value.access_token);
        Assert.NotNull(successfulResponse.Value.token_type);
    }

    public async Task HandleAsync_WhenClientIsNotAuthenticated_ShouldReturn401()
    {
        var request = _fixture.Create<TokenRequest>();
        _sut = _fixture.Create<TokenEndpoint>();

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<Ok<ErrorTokenResponse>>(actual);
        var successfulResponse = (Ok<ErrorTokenResponse>)actual;
        Assert.NotNull(successfulResponse.Value);
        Assert.NotNull(successfulResponse.Value.error);
    }
}