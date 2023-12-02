/** \addtogroup Library
 * @{
 */

namespace Khartyko.InsigniaCreator.Library.Utility;

/// <summary>
/// Enum that represents the different kinds of method types that can be associated with Reflection.
/// </summary>
public enum MethodTypes
{
    /// <summary>
    /// Represents the 'get' portion of a property.
    /// </summary>
    PropertySet,
    
    /// <summary>
    /// Represents the 'set' portion of a property.
    /// </summary>
    PropertyGet,
    
    /// <summary>
    /// Represents the 'get' portion of an indexing operator.
    /// </summary>
    IndexerGet,
    
    /// <summary>
    /// Represents the 'set' portion of an indexing operator.
    /// </summary>
    IndexerSet,
    
    /// <summary>
    /// Represents a constructor or ".ctor".
    /// </summary>
    Constructor,
    
    /// <summary>
    /// Represents a Lambda/Anonymous method.
    /// </summary>
    Lambda,
    
    /// <summary>
    /// Represents any other method that is created.
    /// </summary>
    RegularMethod
}

/** @} */