using System;
using System.Collections.Generic;
using Ozcorps.Core.Reflection;

namespace Ozcorps.Core.Tests.Reflection;

public class ClassBuilderTests
{
    [Fact]
    public void CreateObjectTest()
    {
        var _columns = new Dictionary<string, Type>
        {
            {"Id", typeof(int)},
            {"Name", typeof(string)},
        };

        var _object = new ClassBuilder("Ozcorps.Test").
            CreateObject<dynamic>(_columns);

        _object.Id = 25;

        _object.Name = "Orhan";

        var _type = _object.GetType() as Type;

        var _properties = _type.GetProperties();

        int _id = 0;

        string _name = "";

        foreach (var _property in _properties)
        {
            if (_property.Name == "Id")
            {
                _id = _property.GetValue(_object);
            }
            else if (_property.Name == "Name")
            {
                _name = _property.GetValue(_object);
            }
        }

        Assert.Multiple(() => Assert.Equal("Ozcorps.Test", _type.FullName),
            () => Assert.Equal(2, _properties.Length),
            () => Assert.Equal(25, _id),
            () => Assert.Equal("Orhan", _name));
    }
}