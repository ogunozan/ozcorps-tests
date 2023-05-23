using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Core.Encryptors;

namespace Ozcorps.Core.Tests.Encryptors;

public class Md5EncryptorTests
{
    [Fact]
    public void EncryptDecryptTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddMd5Encryptor();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IEncryptor>();

        var _expected = "hobaa";

        var _encrypted = _service.Encrypt(_expected);

        var _decrypted = _service.Decrypt(_encrypted);

        Assert.Equal(_expected, _decrypted);
    }
}