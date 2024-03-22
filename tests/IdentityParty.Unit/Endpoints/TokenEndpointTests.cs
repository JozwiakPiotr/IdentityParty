using AutoFixture;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Endpoints.V1.Token;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using SuccessfulTokenResponse = IdentityParty.Core.DTO.SuccessfulTokenResponse;
using TokenRequest = IdentityParty.Core.Endpoints.V1.Token.Contract.TokenRequest;

namespace IdentityParty.Unit;

public class TokenEndpointTests
{
    private Fixture _fixture;
    private TokenEndpoint _sut;
    private readonly Mock<IGrantTypeHandler> _grantTypeHandlerMock = new();

    public TokenEndpointTests()
    {
        _fixture = FixtureFactory.GetFixture();
    }

    [Fact]
    public async Task HandleAsync_WhenRequestIsValid_ShouldReturnSuccessfulResponse()
    {
        var request = _fixture.Create<TokenRequest>();
        var successfulResponseDto = _fixture
            .Create<SuccessfulTokenResponse>();
        TokenResponse tokenResponseDto = successfulResponseDto;
        _grantTypeHandlerMock.SetupGet(x => x.GrantType)
            .Returns(request.grant_type);
        _grantTypeHandlerMock.Setup(x => x.HandleAsync(
            It.IsAny<Core.DTO.TokenRequest>())
        ).ReturnsAsync(tokenResponseDto);
        //TODO: use AutoFixture to create sut object
        _sut = new TokenEndpoint(new List<IGrantTypeHandler> { _grantTypeHandlerMock.Object });

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<Ok<Core.Endpoints.V1.Token.Contract.SuccessfulTokenResponse>>(actual);
        var successfulResponse = (Ok<Core.Endpoints.V1.Token.Contract.SuccessfulTokenResponse>)actual;
        Assert.NotNull(successfulResponse.Value);
        Assert.NotNull(successfulResponse.Value.access_token);
        Assert.NotNull(successfulResponse.Value.token_type);
    }

    [Theory]
    [MemberData(nameof(ErrorCodesWith400))]
    public async Task HandleAsync_WhenRequestIsInvalid_ShouldReturn400(string errorCode)
    {
        var request = _fixture.Create<TokenRequest>();
        TokenResponse tokenResponseDto = _fixture
            .Build<ErrorTokenResponse>()
            .With(x => x.Error, errorCode)
            .Create();
        _grantTypeHandlerMock.SetupGet(x => x.GrantType)
            .Returns(request.grant_type);
        _grantTypeHandlerMock.Setup(x => x.HandleAsync(
            It.IsAny<Core.DTO.TokenRequest>())
        ).ReturnsAsync(tokenResponseDto);
        //TODO: use AutoFixture to create sut object
        _sut = new TokenEndpoint(new List<IGrantTypeHandler> { _grantTypeHandlerMock.Object });

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<BadRequest<Core.ErrorTokenResponse>>(actual);
        var errorResponse = (BadRequest<Core.ErrorTokenResponse>)actual;
        Assert.Equal(errorCode, errorResponse.Value?.error);
    }

    [Fact]
    public async Task HandleAsync_WhenInvalidGrantType_ShouldReturn400()
    {
        const string expectedErrorCode = "unsupported_grant_type";
        var request = _fixture.Create<TokenRequest>();
        _sut = new TokenEndpoint(Enumerable.Empty<IGrantTypeHandler>());

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<BadRequest<Core.ErrorTokenResponse>>(actual);
        var errorResponse = (BadRequest<Core.ErrorTokenResponse>)actual;
        Assert.Equal(expectedErrorCode, errorResponse.Value?.error);
    }
    
    public async Task HandleAsync_WhenClientIsNotAuthenticated_ShouldReturn401()
    {
        var request = _fixture.Create<TokenRequest>();
        _sut = _fixture.Create<TokenEndpoint>();

        var actual = await _sut.HandleAsync(request);

        Assert.IsType<Ok<Core.ErrorTokenResponse>>(actual);
        var successfulResponse = (Ok<Core.ErrorTokenResponse>)actual;
        Assert.NotNull(successfulResponse.Value);
        Assert.NotNull(successfulResponse.Value.error);
    }

    public static IEnumerable<object[]> ErrorCodesWith400()
    {
        yield return new object[] { "invalid_request" };
        yield return new object[] { "invalid_grant" };
        yield return new object[] { "unauthorized_client" };
        yield return new object[] { "invalid_scope" };
    }
}