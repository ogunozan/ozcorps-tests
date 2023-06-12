using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Ozcorps.Ozt.OztActionFilter;

namespace Ozcorps.Ozt.UnitTest;

public class OztActionFilterTests
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
    public async void OztActionFilter_InvalidOzt()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        var _httpContext = new DefaultHttpContext();

        _httpContext.Request.Headers.Add("ozt", "something");

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        Assert.Multiple(
            () => Assert.False(_isExecuted),
            () => Assert.Equal("invalid ozt", _result),
            () => Assert.Equal((int)HttpStatusCode.Unauthorized, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_MissingOzt()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        var _httpContext = new DefaultHttpContext();

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        Assert.Multiple(
            () => Assert.False(_isExecuted),
            () => Assert.Equal("missing ozt", _result),
            () => Assert.Equal((int)HttpStatusCode.Unauthorized, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_NoAuthorityForRole()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        _actionFilter.Roles = "superuser";

        var _httpContext = new DefaultHttpContext();

        var _token = _service.GenerateToken(_OztUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        Assert.Multiple(
            () => Assert.False(_isExecuted),
            () => Assert.Equal("no authority in ozt", _result),
            () => Assert.Equal((int)HttpStatusCode.Unauthorized, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_AuthorityForRole()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        _actionFilter.Roles = "superuser,admin";

        var _httpContext = new DefaultHttpContext();

        var _token = _service.GenerateToken(_OztUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        var _oztUser = _httpContext.GetOztUser();

        Assert.Multiple(
            () => Assert.True(_isExecuted),
            () => Assert.Empty(_result),
            () => Assert.Equal(_OztUser.UserId, _oztUser.UserId),
            () => Assert.Equal((int)HttpStatusCode.OK, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_NoAuthorityForPermission()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        _actionFilter.Permissions = "d";

        var _httpContext = new DefaultHttpContext();

        var _token = _service.GenerateToken(_OztUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        Assert.Multiple(
            () => Assert.False(_isExecuted),
            () => Assert.Equal("no authority in ozt", _result),
            () => Assert.Equal((int)HttpStatusCode.Unauthorized, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_AuthorityForPermission()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        _actionFilter.Permissions = "d,a";

        var _httpContext = new DefaultHttpContext();

        var _token = _service.GenerateToken(_OztUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        var _oztUser = _httpContext.GetOztUser();

        Assert.Multiple(
            () => Assert.True(_isExecuted),
            () => Assert.Empty(_result),
            () => Assert.Equal(_OztUser.UserId, _oztUser.UserId),
            () => Assert.Equal((int)HttpStatusCode.OK, _httpContext.Response.StatusCode));
    }

    [Fact]
    public async void OztActionFilter_Authority()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                    AddJsonFile("appsettings.json", true, true).
                    Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddControllers();

        _services.AddLogging();

        _services.AddOzt();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<OztTool>();

        var _actionFilter = new OztActionFilter();

        var _httpContext = new DefaultHttpContext();

        var _token = _service.GenerateToken(_OztUser);

        _httpContext.Request.Headers.Add("ozt", _token);

        _httpContext.RequestServices = _provider;

        _httpContext.ServiceScopeFactory = _provider.GetService<IServiceScopeFactory>();

        var _actionContext = new ActionContext(_httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var _actionExecutingContext = new ActionExecutingContext(_actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object>(),
            controller: null);

        var _isExecuted = false;

        ActionExecutionDelegate _delegate = () =>
        {
            _isExecuted = true;

            return Task.FromResult(new ActionExecutedContext(_actionContext,
                new List<IFilterMetadata>(), null));
        };

        await _actionFilter.OnActionExecutionAsync(_actionExecutingContext, _delegate);

        var _result = "";

        if (_actionExecutingContext.Result != null)
        {
            await _actionExecutingContext.Result.ExecuteResultAsync(_actionContext);

            _result = (_actionExecutingContext.Result as OztFailureResult).Phrase;
        }

        var _oztUser = _httpContext.GetOztUser();

        Assert.Multiple(
            () => Assert.True(_isExecuted),
            () => Assert.Empty(_result),
            () => Assert.Equal(_OztUser.UserId, _oztUser.UserId),
            () => Assert.Equal((int)HttpStatusCode.OK, _httpContext.Response.StatusCode));
    }
}