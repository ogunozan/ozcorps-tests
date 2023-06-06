using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ozcorps.Core.Encryptors;

namespace Ozcorps.Core.Tests.Encryptors;

public class RsaEncryptorTests
{
    [Fact]
    public void GenerateKeysTest()
    {
        var _keys = RsaEncryptor.GenerateRsaKeys();

        Assert.True(_keys.Length == 2);
    }

    [Fact]
    public void EncryptDecryptTest()
    {
        IConfiguration _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.json", true, true).
            Build();

        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton(_configuration);

        _services.AddRsaEncryptor();

        var _provider = _services.BuildServiceProvider();

        var _service = _provider.GetService<IEncryptor>();

        var _expected = "hobaa";

        var _encrypted = _service.Encrypt(_expected);

        var _decrypted = _service.Decrypt(_encrypted);

        Assert.Equal(_expected, _decrypted);
    }
}