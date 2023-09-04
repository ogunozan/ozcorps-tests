using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Tools.Email;

namespace Ozcorps.Tools.Tests;

public class EmailToolTests
{
    [Fact]
    public void SendPlainTextEmailTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddEmailTool();

        var _provider = _services.BuildServiceProvider();

        var _tool = _provider.GetService<EmailTool>();

        var _emailMessage = new EmailMessage(_tool.Configuration.From,
            "ogunozan@gmail.com",
            "subject",
            "plain text"
        );

        _tool.SendEmail(_emailMessage);

        Assert.True(true);
    }

    [Fact]
    public void SendHtmlEmailsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddEmailTool();

        var _provider = _services.BuildServiceProvider();

        var _tool = _provider.GetService<EmailTool>();

        var _html = "<!DOCTYPE html>" +
            "<html>" +
            "<body>" +
            "<h1>html</h1>" +
            "<p>email sample</p>" +
            "</body>" +
            "</html>";

        var _emailMessage = new EmailMessage(_tool.Configuration.From,
            new List<string> { "ogunozan@gmail.com", "alprkamu@gmail.com" },
            "subject",
            _html,
            true
            );

        _tool.SendEmail(_emailMessage);

        Assert.True(true);
    }

    [Fact]
    public void SendAlternateViewEmailsTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddEmailTool();

        var _provider = _services.BuildServiceProvider();

        var _tool = _provider.GetService<EmailTool>();

        var _html = File.ReadAllText(Path.Combine("contents", "email-sample.html"));

        var _inlineLogo = new LinkedResource(Path.Combine("contents", "logo.png"), "image/png")
        {
            ContentId = Guid.NewGuid().ToString()
        };

        _html = _html.Replace("[logo]", _inlineLogo.ContentId);

        var _view = AlternateView.CreateAlternateViewFromString(_html, null, "text/html");

        _view.LinkedResources.Add(_inlineLogo);

        var _emailMessage = new EmailMessage(_tool.Configuration.From,
            new List<string> { "ogunozan@gmail.com", "alprkamu@gmail.com" },
            "subject",
            _view
            );

        _tool.SendEmail(_emailMessage);

        Assert.True(true);
    }

    [Fact]
    public void SendAlternateViewEmailTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddEmailTool();

        var _provider = _services.BuildServiceProvider();

        var _tool = _provider.GetService<EmailTool>();

        var _html = File.ReadAllText(Path.Combine("contents", "email-sample.html"));

        var _inlineLogo = new LinkedResource(Path.Combine("contents", "logo.png"), "image/png")
        {
            ContentId = Guid.NewGuid().ToString()
        };

        _html = _html.Replace("[logo]", _inlineLogo.ContentId);

        var _view = AlternateView.CreateAlternateViewFromString(_html, null, "text/html");

        _view.LinkedResources.Add(_inlineLogo);

        var _emailMessage = new EmailMessage(_tool.Configuration.From,
            "ogunozan@gmail.com",
            "subject",
            _view
            );

        _tool.SendEmail(_emailMessage);

        Assert.True(true);
    }

    [Fact]
    public async void SendEmailAsyncTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddEmailTool();

        var _provider = _services.BuildServiceProvider();

        var _tool = _provider.GetService<EmailTool>();

        var _emailMessage = new EmailMessage(_tool.Configuration.From,
            "ogunozan@gmail.com",
            "subject",
            "plain text"
        );

        await _tool.SendEmailAsync(_emailMessage);

        Assert.True(true);
    }
}