/** \addtogroup Domain
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interfaces;

/// <summary>
/// This abstracts the type away so it can be used in a more general sense.
/// </summary>
public interface INetworkGenerator
{
}

/// <summary>
/// Declares a method that generates a TemplateNetwork with data of the specified type.
/// </summary>
/// <typeparam name="TGenerationData">The specified type of TemplateNetwork to generate.</typeparam>
public interface INetworkGenerator<in TGenerationData> : INetworkGenerator
    where TGenerationData : NetworkData
{
    /// <summary>
    /// Generates a TemplateNetwork with the given data.
    /// </summary>
    /// <param name="generationData">The data used in generating a TemplateNetwork.</param>
    /// <returns>The newly generated TemplateNetwork.</returns>
    TemplateNetwork GenerateNetwork(TGenerationData generationData);
}

/** @} */