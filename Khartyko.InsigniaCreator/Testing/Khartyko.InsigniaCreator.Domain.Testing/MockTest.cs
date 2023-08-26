using System.Reflection;
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

namespace Khartyko.InsigniaCreator.Domain.Testing;

public class MockTest
{
    private string _name = "Kjotvyko";

    public string Name
    {
        get
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

            MethodInfo? methodInfo = GetType()
                .GetMethod(metadata.MethodBase.Name);

             return _name;
        }
        set
        {
            ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

            MethodInfo? methodInfo = GetType()
                .GetMethod(metadata.MethodBase.Name);

            _name = value;
        }
    }

    [Fact]
    public void PassingTest()
    {
        Name = "Khartyko";
        _ = Name;
    }
}