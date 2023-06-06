using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Tools.Tests;

public class LdapTests
{
    [Fact]
    public void CheckTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddLdapTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<LdapTool>();

        var _result = _service.Check("oz", "123456");

        Assert.True(_result == Ldap.LdapResult.VALIDATE || 
            _result == Ldap.LdapResult.NOT_CHECKED);
    }
}