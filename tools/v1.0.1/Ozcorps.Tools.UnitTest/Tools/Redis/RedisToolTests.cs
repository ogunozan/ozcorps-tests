using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Tools.Tests;

public class RedisToolTests
{
    private class RedisItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public RedisItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }

        public RedisItem()
        {
            
        }
    }

    private readonly RedisItem _Sample = new RedisItem(
        new Random().Next(), "oz", 25, DateTime.Now, true);

    private const string _TestKey = "my-test-key";

    [Fact]
    public void SetGetTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRedisTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<RedisTool>();

        _service.Set(_TestKey, _Sample);

        var _result = _service.Get<RedisItem>(_TestKey);

        Assert.Equal(_Sample.Id, _result.Id);
    }

    [Fact]
    public void DeleteTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRedisTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<RedisTool>();
        
        var _result = _service.Delete(_TestKey);

        Assert.True(_result);
    }
}