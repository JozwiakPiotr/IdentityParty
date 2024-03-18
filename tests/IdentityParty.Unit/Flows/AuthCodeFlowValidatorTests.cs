using IdentityParty.Core.DTO;
using IdentityParty.Core.Flows.AuthCode;

namespace IdentityParty.Unit;

public class AuthCodeFlowValidatorTests
{
    

    // [Theory]
    // [MemberData(nameof(InvalidRequests))]
    // internal async Task ValidateAsync_WhenRequestIsInvalid_ShouldReturnNotNullErrorResponse(
    //     AuthorizationRequest request,
    //     ErrorAuthorizationResponse err)
    // {
    //     var actual = await _sut.Validate(request);

    //     Assert.Equal(err, actual);
    // }

    // [Theory]
    // [MemberData(nameof(ValidRequests))]
    // public async Task ValidateAsync_WhenRequestIsValid_ShouldReturnNull()
    // {

    // }

    // public static IEnumerable<object[]> InvalidRequests()
    // {
    //     yield return new object[]{};
    // }

    // public static IEnumerable<object[]> ValidRequests()
    // {
    //     yield return new object[]{};
    // }
}
