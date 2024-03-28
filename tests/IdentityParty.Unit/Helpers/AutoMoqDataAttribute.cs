using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace IdentityParty.Unit;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(FixtureFactory.Create)
    {
    }
}
