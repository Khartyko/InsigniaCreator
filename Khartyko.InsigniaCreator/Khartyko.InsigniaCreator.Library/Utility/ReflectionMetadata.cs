/** \addtogroup Library
 * @{
 */
using System.Reflection;

namespace Khartyko.InsigniaCreator.Library.Utility;

/// <summary>
/// Class that holds data that describes a method within C#.
/// </summary>
/// <remarks>
/// This is primarily intended to be used with logging and debugging.
/// </remarks>
public class ReflectionMetadata
{
    /// <summary>
    /// Constructs an object with the given data about a method.
    /// </summary>
    /// <param name="type">The type of the class that declares the method.</param>
    /// <param name="methodBase">The method data used in Reflectino.</param>
    /// <param name="methodType">The type of method.</param>
    public ReflectionMetadata(Type type, MethodBase methodBase, MethodTypes methodType)
    {
        Type = type;
        MethodBase = methodBase;
        MethodType = methodType;
    }

    /// <summary>
    /// The declaring Type that has the method.
    /// </summary>
    public Type Type { get; }
    
    /// <summary>
    /// The method info that is retrieved.
    /// </summary>
    public MethodBase MethodBase { get; }
    
    /// <summary>
    /// The type of method that this class describes.
    /// </summary>
    public MethodTypes MethodType { get; }
}
/** @} */