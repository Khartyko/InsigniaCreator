using System.Reflection;
namespace Khartyko.InsigniaCreator.Library.Utility;

public class ReflectionMetadata
{
    public ReflectionMetadata(Type type, MethodBase methodBase)
    {
        Type = type;
        MethodBase = methodBase;
    }

    public Type Type { get; set; }
    public MethodBase MethodBase { get; set; }
}