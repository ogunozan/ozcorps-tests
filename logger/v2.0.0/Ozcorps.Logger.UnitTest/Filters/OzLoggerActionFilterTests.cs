using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Logger.ApiTest.Controllers;
using Ozcorps.Ozt;

namespace Ozcorps.Logger.UnitTest;

public class OzLoggerActionFilterTests
{
    private OztUser _TokenUser = new OztUser
    {
        Email = "test@gmail.com",
        Permissions = new List<string> { "something" },
        RoleId = 1,
        UserId = 1,
        Roles = new List<string> { "admin" },
        Username = "oz"
    };

    [Fact]
    public async void OzLoggerActionFilter_WithoutUser()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddOzTextLogger();

        var _provider = _services.BuildServiceProvider();

        var _logger = _provider.GetService<IOzLogger>();

        var _actionFilter = new OzLoggerActionFilter();

        var _httpContext = new DefaultHttpContext();

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ControllerActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            null);

        var _resultExecutingContext = new ResultExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new OkObjectResult("it's done"),
            new OzLoggerActionFilterTestController(_logger, null));

        ActionExecutionDelegate _actionDelegate = () =>
        {
            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        ResultExecutionDelegate _resultDelegate = () =>
        {
            return Task.FromResult(new ResultExecutedContext(_actionContext,
                new List<IFilterMetadata>(), new OkResult(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _actionDelegate);

        await _actionFilter.OnResultExecutionAsync(_resultExecutingContext, _resultDelegate);
    }

    [Fact]
    public async void OzLoggerActionFilter()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddOzTextLogger();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _logger = _provider.GetService<IOzLogger>();

        var _ozt = _provider.GetService<OztTool>();

        var _oztActionFilter = new OztActionFilter();

        var _loggerActionFilter = new OzLoggerActionFilter();

        var _httpContext = new DefaultHttpContext();

        var _token = _ozt.GenerateToken(_TokenUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ControllerActionDescriptor()
            {
                ControllerName = "my-controller",
                ActionName = "my-action"
            },
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            null);

        var _resultExecutingContext = new ResultExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new OkObjectResult("it's done"),
            new OzLoggerActionFilterTestController(_logger, null));

        ActionExecutionDelegate _actionDelegate = () =>
        {
            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        ResultExecutionDelegate _resultDelegate = () =>
        {
            return Task.FromResult(new ResultExecutedContext(_actionContext,
                new List<IFilterMetadata>(), new OkResult(), null));
        };

        await _oztActionFilter.OnActionExecutionAsync(_actionExecutingContext, _actionDelegate);

        await _loggerActionFilter.OnActionExecutionAsync(_actionExecutingContext, _actionDelegate);

        await _loggerActionFilter.OnResultExecutionAsync(_resultExecutingContext, _resultDelegate);
    }
}