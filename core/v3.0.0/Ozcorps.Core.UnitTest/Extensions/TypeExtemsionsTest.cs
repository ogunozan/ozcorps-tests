using Ozcorps.Core.Extensions;
using System;
using System.Linq;

namespace Ozcorps.Core.Tests.Extensions;

public class TypeExtemsionsTest
{
    [DescriptionAttribute("class for type extensions test")]
    private class TypeItem
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime Date { get; init; }        
        public bool IsActive { get; init; }
    }

    private class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string _description) =>
            Description = _description;

        public string Description { get; set; }
    }
    
    [Fact]
    public void GetPropertyNamesTest()
    {
        var _expected = "IsActive";

        var _result = typeof(TypeItem).GetPropertyNames();

        Assert.Contains(_expected, _result);
    }

    [Fact]
    public void FindAttributesTest()
    {
        var _expected = "class for type extensions test";

        var _result = typeof(TypeItem).FindAttributes<DescriptionAttribute>().
            Select(x=>x.Description);

        Assert.Contains(_expected, _result);
    }
}