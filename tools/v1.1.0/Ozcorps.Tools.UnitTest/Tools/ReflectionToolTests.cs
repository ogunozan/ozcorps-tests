using System.Linq;

namespace Ozcorps.Tools.Tests;

public class ReflectionToolTests
{
    public interface TestInterface
    {
        public int Id { get; set; }
    }

    public class TestClass : TestInterface
    {
        public int Id { get; set; }
    }

    [Fact]
    public void GetTypesInNamespaceTest()
    {
        var _assymbly = System.Reflection.Assembly.GetExecutingAssembly();

        var _result = ReflectionTool.GetTypesInNamespace(_assymbly, 
            "Ozcorps.Tools.Tests", 
            _implementedInterface: typeof(TestInterface));

        Assert.Equal(typeof(TestClass), _result.First());
    }
}