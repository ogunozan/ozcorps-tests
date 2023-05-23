using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Core.Models;

namespace Ozcorps.Core.Tests.Models;

public class ResponseTests
{
    [Fact]
    public void ResponseDefaultTest()
    {
        var _response = new Response();

        Assert.Multiple(() => Assert.Equal("something went wrong!", _response.Message),
            () => Assert.False(_response.Success),
            () => Assert.Null(_response.Data));
    }

    [Fact]
    public void ResponseConfigTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
         AddJsonFile("appsettings.json", true, true).
         Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        var _response = new Response(_configuration);

        Assert.Multiple(
            () => Assert.Equal(_configuration["DefaultResponseMessage"], _response.Message),
            () => Assert.False(_response.Success),
            () => Assert.Null(_response.Data));
    }
}