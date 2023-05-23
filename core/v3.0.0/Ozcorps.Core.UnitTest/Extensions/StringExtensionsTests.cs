using Ozcorps.Core.Extensions;

namespace Ozcorps.Core.Tests.Extensions;

public class StringExtensionsTests
{
    [Fact]
    public void ToUpperFirstLetterTest()
    {
        var _sample = "firstletter";
        
        var _expected = "Firstletter";
        
        var _result = _sample.ToUpperFirstLetter();

        Assert.Equal(_expected, _result);
    }
}