using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Ozt.UnitTest;

public class OztActionFilterTest
{
    private OztUser _OztUser = new OztUser
    {
        Email = "orhan@gmail.com",
        Permissions = new List<string> { "a", "b", "c" },
        RoleId = 1,
        Roles = new List<string> { "admin" },
        UserId = 1,
        IsLdapLogin = false,
        Username = "orhan"
    };

    [Fact]
    public void GenereateOztTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _result = _service.GenerateToken(_OztUser);

        Assert.False(string.IsNullOrEmpty(_result));
    }

    [Fact]
    public void ValidateOztTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _token = _service.GenerateToken(_OztUser);

        var _result = _service.ValidateToken(_token);

        Assert.Multiple(()=>Assert.True(_result.IsValidated),
            ()=> Assert.Equal(_OztUser.UserId, _result.OztUser.UserId));
    }
}