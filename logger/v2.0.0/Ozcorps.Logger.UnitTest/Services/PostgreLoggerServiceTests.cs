using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Logger.Db.Postgre;

namespace Ozcorps.Logger.UnitTest;

public class PostgreLoggerServiceTests
{
    [Fact]
    public void GetActionLogsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzPostgreLogger(_configuration.GetConnectionString("PostgreLogger"));

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IPostgreLoggerService>();

        var _logs = _service.GetActionLogs().ToList();
    }

    [Fact]
    public void GetAuditLogsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzPostgreLogger(_configuration.GetConnectionString("PostgreLogger"));

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IPostgreLoggerService>();

        var _logs = _service.GetAuditLogs().ToList();
    }

    [Fact]
    public void GetOtherLogsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzPostgreLogger(_configuration.GetConnectionString("PostgreLogger"));

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IPostgreLoggerService>();

        var _logs = _service.GetOtherLogs().ToList();
    }

    [Fact]
    public void GetUserLogsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddOzPostgreLogger(_configuration.GetConnectionString("PostgreLogger"));

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IPostgreLoggerService>();

        var _logs = _service.GetUserLogs().ToList();
    }
}