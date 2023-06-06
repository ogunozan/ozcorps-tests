using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Tools.Tests;

public class KafkaToolTests
{
    private class KafkaItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public KafkaItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }

        public KafkaItem()
        {

        }
    }

    private KafkaItem _Sample = new KafkaItem(60, "oz", 25, DateTime.Now, true);

    private string _TestTopic = "test-topic-60";

    [Fact]
    public async void ProduceTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddKafkaTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<KafkaTool>();

        var _result = await _service.ProduceAsync(new Guid().ToString(), _Sample);

        Assert.True(_result == Confluent.Kafka.PersistenceStatus.Persisted);
    }

    [Fact]
    public async void ConsumeTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddKafkaTool();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<KafkaTool>();

        await _service.ProduceAsync(_TestTopic, _Sample);

        await _service.ConsumeAsync<KafkaItem>(_TestTopic, (_result) =>
            {
                Assert.Equal(_Sample.Id, _result.Id);
            }, true);
    }
}