using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Ozcorps.Core.ElasticSearch;

namespace Ozcorps.Core.Tests.ElasticSearch;

public class EleasticSearchTests
{
    private class ElasticItem
    {
        public long Id { get; set; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime Date { get; init; }
        public bool IsActive { get; init; }

        public ElasticItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }
    }

    private const string _TestIndex = "my-test-index";

    [Fact]
    public void AddTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddElasticSearchProvider();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IElasticSearchService<ElasticItem>>();

        var _item = new ElasticItem(1, "oz", 25, DateTime.Now, true);

        var _response = _service.Add(_item, _TestIndex);

        Assert.True(_response);
    }

    [Fact]
    public void BulkTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddElasticSearchProvider();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IElasticSearchService<ElasticItem>>();

        var _list = new List<ElasticItem>
        {
            new ElasticItem(1, "veli", 25, DateTime.Now, true),
            new ElasticItem(2, "ali", 35, DateTime.Now.AddDays(-1), false),
        };

        var _response = _service.Bulk(_list, _TestIndex);

        Assert.True(_response);
    }

    [Fact]
    public void CheckConntectionTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddElasticSearchProvider();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IElasticSearchService<ElasticItem>>();

        var _response = _service.CheckConnection();

        Assert.True(_response);
    }

    [Fact]
    public void SearchTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddElasticSearchProvider();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IElasticSearchService<ElasticItem>>();

        var _response = _service.Search(_TestIndex);

        Assert.NotNull(_response);
    }

    [Fact]
    public void GetClientTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddElasticSearchProvider();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IElasticSearchService<ElasticItem>>();

        var _response = _service.GetClient();

        Assert.NotNull(_response);
    }
}