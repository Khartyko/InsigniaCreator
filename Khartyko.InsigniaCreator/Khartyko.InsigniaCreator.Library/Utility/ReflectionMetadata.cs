using System.Reflection;

namespace Khartyko.InsigniaCreator.Library.Utility;

public class ReflectionMetadata
{
    public ReflectionMetadata(Type type, MethodBase methodBase, MethodTypes methodType)
    {
        Type = type;
        MethodBase = methodBase;
        MethodType = methodType;
    }

    public Type Type { get; set; }
    public MethodBase MethodBase { get; set; }
    public MethodTypes MethodType { get; set; }
}