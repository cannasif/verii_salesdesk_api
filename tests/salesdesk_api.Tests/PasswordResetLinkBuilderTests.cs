using Microsoft.Extensions.Configuration;
using salesdesk_api.Modules.Identity.Application;
using Xunit;

namespace salesdesk_api.Tests;

public class PasswordResetLinkBuilderTests
{
    [Fact]
    public void Build_UsesClientResetUrlWithoutQuery()
    {
        var config = new ConfigurationBuilder().Build();
        var link = PasswordResetLinkBuilder.Build(
            "https://salesdesk.v3rii.com/reset-password?old=1",
            null,
            "abc123",
            config);

        Assert.Equal("https://salesdesk.v3rii.com/reset-password?token=abc123", link);
    }

    [Fact]
    public void Build_FallsBackToFrontendSettingsWhenClientUrlMissing()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["FrontendSettings:BaseUrl"] = "https://salesdesk.v3rii.com",
                ["FrontendSettings:ResetPasswordPath"] = "/reset-password",
            })
            .Build();

        var link = PasswordResetLinkBuilder.Build(null, null, "tok", config);

        Assert.Equal("https://salesdesk.v3rii.com/reset-password?token=tok", link);
    }

    [Fact]
    public void Build_PrefersResetUrlOverResetPasswordUrl()
    {
        var config = new ConfigurationBuilder().Build();
        var link = PasswordResetLinkBuilder.Build(
            "https://a.example/reset",
            "https://b.example/reset",
            "t",
            config);

        Assert.Equal("https://a.example/reset?token=t", link);
    }
}
