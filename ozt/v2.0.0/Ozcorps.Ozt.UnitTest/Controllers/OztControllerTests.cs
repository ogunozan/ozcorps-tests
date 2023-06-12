using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Ozt.ApiTest.Controllers;

namespace Ozcorps.Ozt.UnitTest;

public class OztControllerTests
{
    [Fact]
    public void GenerateOztTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _controller = new OztTestController(_service);

        var _response = _controller.GenerateOzt(1);

        Assert.Multiple(() => Assert.True(_response.Success),
            () => Assert.True(!string.IsNullOrEmpty(_response.Data.ToString())));
    }

    [Fact]
    public void ValidateHeaderOzt()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _controller = new OztTestController(_service)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var _userId = 2;

        var _user = _controller._Users.FirstOrDefault(x=>x.UserId == 2);

        var _token = _controller.GenerateOzt(_userId);

        _controller.Request.Headers.Add("ozt", _token.Data.ToString());

        var _response = _controller.ValidatOztManual();

        var _data = _response.Data as OztValidation;

        Assert.Multiple(() => Assert.True(_response.Success),
            () => Assert.True(_data.IsValidated),
            () => Assert.Equal(_userId, _data.OztUser.UserId),
            () => Assert.Equal(_user.Username, _data.OztUser.Username));
    }

    [Fact]
    public void ValidateHeaderOzt_Fail()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _controller = new OztTestController(_service)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        _controller.Request.Headers.Add("ozt", "somehting");

        var _response = _controller.ValidatOztManual();

        var _data = _response.Data as OztValidation;

        Assert.Multiple(() => Assert.True(_response.Success),
            () => Assert.False(_data.IsValidated));
    }    
}