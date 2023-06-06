namespace Ozcorps.Tools.Tests;

public class EnumToolTests
{
    public enum SampleEnum
    {
        VALIDATE,
        INVALIDATE
    }

    [Fact]
    public void StringToEnumTest()
    {
        var _result = EnumTool.StringToEnum<SampleEnum>("VALIDATE");

        Assert.Equal(SampleEnum.VALIDATE, _result);
    }

    [Fact]
    public void GetEnumItems()
    {
        var _result = EnumTool.GetEnumItems<SampleEnum>();

        Assert.Multiple(()=>Assert.Equal(2, _result.Count),
            ()=> Assert.Contains(SampleEnum.VALIDATE, _result));
    }
}