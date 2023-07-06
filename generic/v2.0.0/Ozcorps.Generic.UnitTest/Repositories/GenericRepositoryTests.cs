using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ozcorps.Generic.ApiTest.Dal;
using Ozcorps.Generic.ApiTest.Entity;
using Ozcorps.Core.Extensions;
using Ozcorps.Generic.Dal;

namespace Ozcorps.Generic.UnitTest;

public class GenericRepositoryTests
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.Get(_adddedPoiId);

        Assert.Equal(_adddedPoiId, _result.Id);
    }

    [Fact]
    public void GetByPredicateTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.Get(x => x.Id == _adddedPoiId);

        Assert.Equal(_adddedPoiId, _result.Id);
    }

    [Fact]
    public void GetFirstTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.GetFirst(x => x.Id == _adddedPoiId);

        Assert.Equal(_adddedPoiId, _result.Id);
    }

    [Fact]
    public void AnyTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.Any(x => x.Id == _adddedPoiId);

        Assert.True(_result);
    }

    [Fact]
    public void GetAllTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.GetAll();

        Assert.True(_result.Count() > 0);
    }

    [Fact]
    public void GetAllByPredicateTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.GetAll(x => x.Name == "Ankara");

        Assert.True(_result.Count() > 0);
    }

    [Fact]
    public void GetQueryableTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _adddedPoiId = AddTest();

        var _result = _repository.GetQueryable();

        Assert.True(_result.Count() > 0);
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _result = _repository.Add(new Poi
        {
            Geoloc = "POINT(32.67578125 40.04852042283317)".WktToGeometry(),
            Name = "Ankara"
        });

        _unitOfWork.Save();

        Assert.True(_result.Id > 0);

        return _result.Id;
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _addedId = AddTest();

        _repository.Remove(_addedId);
    }

    [Fact]
    public void RemoveByEntityTest()
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

        var _unitOfWork = _provider.GetService<IUnitOfWork>();

        var _repository = _unitOfWork.GetRepository<Poi>();

        var _addedId = AddTest();

        var _entity = _repository.Get(_addedId);

        _repository.Remove(_entity);
    }
}