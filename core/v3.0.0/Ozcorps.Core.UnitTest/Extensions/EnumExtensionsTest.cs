using Ozcorps.Core.Extensions;
using System.ComponentModel;

namespace Ozcorps.Core.Tests.Extensions;

public class EnumExtensionsTest
{
    private enum TestEnum
    {
        [DescriptionAttribute("a1")]
        BEGINNER,

        [DescriptionAttribute("a2")]
        ELEMENTARY,

        [DescriptionAttribute("b1")]
        INTERMEDIATE,

        [DescriptionAttribute("b2")]
        UPPER_INTERMEDIATE,

        [DescriptionAttribute("c1")]
        ADVENCED,

        [DescriptionAttribute("1,3,5,7")]
        PROFICIENCY
    }

    [Fact]
    public void ToDescriptionStringTest()
    {
        var _description = TestEnum.ELEMENTARY.ToDescriptionString();

        var _expected = "a2";

        Assert.Equal(_expected, _description);
    }

    [Fact]
    public void ToIds()
    {
        var _ids = TestEnum.PROFICIENCY.ToIds();

        Assert.Multiple(() => Assert.Equal(4, _ids.Count),
            () => Assert.Equal(5, _ids[2]));
    }
}