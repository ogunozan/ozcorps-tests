using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Generic.Bll;
using Microsoft.EntityFrameworkCore;
using Ozcorps.Generic.ApiTest.Dal;
using System.Collections.Generic;
using Ozcorps.Generic.ApiTest.Entity;
using Ozcorps.Core.Extensions;

namespace Ozcorps.Generic.UnitTest;

public class GenericServiceTests
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _entityService.Get(_adddedPoiId);

        Assert.Equal(_adddedPoiId, _result.Id);
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        var _result = _entityService.Add(new Dto.AddEntityDto
        {
            Columns = new Dictionary<string, object> { ["Name"] = "Ankara" },
            Wkt = "POINT(32.67578125 40.04852042283317)"
        }, 0);

        Assert.True(_result.Id > 0);

        return _result.Id;
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        var _addedId = AddTest();

        var _result = _entityService.Update(new Dto.UpdateEntityDto
        {
            Id = _addedId,
            Columns = new Dictionary<string, object> { ["Name"] = "AnkaraYeni" },
            Wkt = "POINT (32 40)"
        }, 0);

        Assert.Multiple(() => Assert.Equal("AnkaraYeni", _result.Name),
            () => Assert.Equal("POINT (32 40)", _result.Geoloc.ToWkt()));
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        var _addedId = AddTest();

        _entityService.Remove(_addedId, 0);
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        AddTest();

        var _wkt = "POLYGON((27.74789673581876 41.50459213620306," +
            "44.57895142331876 41.50459213620306," +
            "44.57895142331876 36.5936167061645," +
            "27.74789673581876 36.5936167061645," +
            "27.74789673581876 41.50459213620306))";

        var _result = _entityService.Intersect(_wkt);

        Assert.True(_result.Count > 0);
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

        var _entityService = _entityServiceProvider.GetEntityService<Poi>();

        AddTest();

        var _result = _entityService.Paginate(new Core.Models.PaginatorDto
        {
            Limit = 10,
            FilterColumns = new string[] { "Name" },
            FilterValues = new string[] { "ka" }
        });

        Assert.True(_result.Count > 0 &&
            _result.Rows.Count() > 0);
    }
}