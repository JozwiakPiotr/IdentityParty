using AutoFixture;
using AutoFixture.AutoMoq;

namespace IdentityParty.Unit;

public static class FixtureFactory
{
    public static Fixture GetFixture()
    {
        var fxt = new Fixture();
        fxt.Customize(new AutoMoqCustomization());
        return fxt;
    }
}
