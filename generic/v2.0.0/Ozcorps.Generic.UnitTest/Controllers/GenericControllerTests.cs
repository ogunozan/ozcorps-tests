using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Generic.Bll;
using Ozcorps.Generic.ApiTest.Controllers;
using Ozcorps.Logger;
using Microsoft.EntityFrameworkCore;
using Ozcorps.Generic.ApiTest.Dal;
using System.Collections.Generic;
using Ozcorps.Generic.ApiTest.Entity;
using Ozcorps.Core.Models;

namespace Ozcorps.Generic.UnitTest;

public class GenericControllerTests
{
    [Fact]
    public void GetTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger);

        var _adddedPoiId = AddTest();

        var _result = _controller.Get(_adddedPoiId);

        Assert.Equal(_adddedPoiId, (_result.Data as dynamic).Id);
    }

    [Fact]
    public long AddTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var _result = _controller.Add(new Dto.AddEntityDto
        {
            Columns = new Dictionary<string, object> { ["Name"] = "Ankara" },
            Wkt = "POINT(32.67578125 40.04852042283317)"
        });

        Assert.True(_result.Success && _result.Data != null);

        return (_result.Data as dynamic).Id;
    }

    [Fact]
    public void UpdateTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var _addedId = AddTest();

        var _result = _controller.Update(new Dto.UpdateEntityDto
        {
            Id = _addedId,
            Columns = new Dictionary<string, object> { ["Name"] = "AnkaraYeni" },
            Wkt = "POINT(32 40)"
        });

        Assert.Multiple(() => Assert.True(_result.Success && _result.Data != null),
            () => Assert.Equal("AnkaraYeni", (_result.Data as dynamic).Name));
    }

    [Fact]
    public void RemoveTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var _addedId = AddTest();

        var _result = _controller.Remove(_addedId);

        Assert.True(_result.Success);
    }

    [Fact]
    public void IntersectTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        AddTest();

        var _wkt = "POLYGON((27.74789673581876 41.50459213620306," +
            "44.57895142331876 41.50459213620306," +
            "44.57895142331876 36.5936167061645," +
            "27.74789673581876 36.5936167061645," +
            "27.74789673581876 41.50459213620306))";

        var _result = _controller.Intersect(_wkt);

        Assert.True(_result.Success && (_result.Data as IList<Poi>).Count > 0);
    }

    [Fact]
    public void PaginateTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
                   AddJsonFile("appsettings.json", true, true).
                   Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddDbContext<DbContext, BaseDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("Postgre"),
                x => x.UseNetTopologySuite()));

        _services.AddOzTextLogger();

        _services.AddGeneric();

        var _provider = _services.BuildServiceProvider();

        var _entityServiceProvider = _provider.GetService<IEntityServiceProvider>();

        var _logger = _provider.GetService<IOzLogger>();

        var _controller = new PoiGenericTestController(_entityServiceProvider, _logger)
        {
            ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        AddTest();

        var _result = _controller.Paginate(new Core.Models.PaginatorDto
        {
            Limit = 10,
            FilterColumns = new string[]{"Name"},
            FilterValues = new string[]{"ka"}
        });

        Assert.True(_result.Success && 
            (_result.Data as PaginatorResponseDto<Poi>).Count > 0 && 
            (_result.Data as PaginatorResponseDto<Poi>).Rows.Count() > 0 );
    }
}