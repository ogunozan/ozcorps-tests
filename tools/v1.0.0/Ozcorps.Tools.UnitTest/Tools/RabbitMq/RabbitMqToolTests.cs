using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Tools.Tests;

public class RabbitMqToolTests
{
    private class RabbitMqItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public RabbitMqItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }

        public RabbitMqItem()
        {
            
        }
    }

    private readonly RabbitMqItem _Sample = new RabbitMqItem(
        new Random().Next(), "oz", 25, DateTime.Now, true);

    private const string _TestQueue = "my-test-queue";

    [Fact]
    public async void PublishTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRabbitMqTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<RabbitMqTool>();

        await _service.PublishAsync(_TestQueue, _Sample);
    }

    [Fact]
    public async void ConsumeTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRabbitMqTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<RabbitMqTool>();

        await _service.PublishAsync(_TestQueue, _Sample);

        _ = _service.ConsumeAsync<RabbitMqItem>(_TestQueue, (_result) =>
            {
                Assert.Equal(_Sample.Id, _result.Id);
            });
    }

    [Fact]
    public async void GetTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRabbitMqTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<RabbitMqTool>();

        var _queue = new Guid().ToString();

        await _service.PublishAsync(_queue, _Sample);
        
        var _result = _service.Get<RabbitMqItem>(_queue);

        Assert.Equal(_Sample.Id, _result.Id);
    }
}