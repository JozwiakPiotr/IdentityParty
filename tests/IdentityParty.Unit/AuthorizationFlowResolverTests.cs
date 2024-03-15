using AutoFixture;
using AutoFixture.AutoMoq;
using IdentityParty.Core.Abstractions.Handlers;
using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows;
using Moq;

namespace IdentityParty.Unit;

public class AuthorizationFlowResolverTests
{
    private AuthorizationFlowResolver _sut;
    private readonly Fixture _fixture;

    public AuthorizationFlowResolverTests()
    {
        _fixture = new Fixture();
        _fixture.Customize(new AutoMoqCustomization());
    }

    [Fact]
    public async Task HandleAsync_WhenResponseTypeIsNotSupported_ShouldReturnErrorResponse()
    {
        var request = _fixture.Build<AuthorizationRequest>()
            .With(x => x.ResponseType, "")
            .Create();
        _sut = _fixture.Create<AuthorizationFlowResolver>();

        var actual = await _sut.HandleAsync(request);

        Assert.NotNull(actual.Error);
        Assert.Equal("unsupported_response_type", actual.Error.Error);
    }

    [Theory]
    [MemberData(nameof(SupportedResponseType))]
    public async Task HandleAsync_WhenResponseTypeIsSupported_ShouldCallResponseTypeHandler(string responseType)
    {
        var request = _fixture.Build<AuthorizationRequest>()
            .With(x => x.ResponseType, responseType)
            .Create();
        var expected = _fixture.Create<AuthorizationResponse>();
        var handlerMock = new Mock<IResponseTypeHandler>();
        handlerMock.SetupGet(x => x.ResponseType).Returns(responseType);
        handlerMock.Setup(x => x.HandleAsync(request)).ReturnsAsync(expected);

        _sut = new AuthorizationFlowResolver(new[] { handlerMock.Object });

        var actual = await _sut.HandleAsync(request);

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> SupportedResponseType()
    {
        yield return new object[] { "code" };
        yield return new object[] { "token" };
    }
}
