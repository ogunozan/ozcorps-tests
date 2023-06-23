using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ozcorps.Logger.UnitTest;

public class RabbitMqLoggerTests
{
    [Fact]
    public void ActionTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.Action(LogSamples.ActionLog);
    }

    [Fact]
    public async void ActionAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.ActionAsync(LogSamples.ActionLog);
    }

    [Fact]
    public void AuditTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.Audit(LogSamples.AuditLogs);
    }

    [Fact]
    public async void AuditAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.AuditAsync(LogSamples.AuditLogs);
    }

    [Fact]
    public void DebugTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.Debug("debug message");
    }

    [Fact]
    public async void DebugAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.DebugAsync("debug async mesage");
    }

    [Fact]
    public void ErrorTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.Error(new NullReferenceException(), "error message");
    }

    [Fact]
    public async void ErrorAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.ErrorAsync(new NullReferenceException(), "error async message");
    }

    [Fact]
    public void UserTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.User(LogSamples.UserLog);
    }

    [Fact]
    public async void UserAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.UserAsync(LogSamples.UserLog);
    }

    [Fact]
    public void WarningTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        _service.Warning("warning message");
    }

    [Fact]
    public async void WarningAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzRabbitMqLogger();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IOzLogger>();

        await _service.WarningAsync("warning async mesage");
    }
}