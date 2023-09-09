using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Library.Entity;

namespace Khartyko.InsigniaCreator.Domain.Interface;

/// <summary>
/// Declares a method that generates a TemplateNetwork with data of the specified type.
/// </summary>
/// <typeparam name="TGenerationData">The specified type of TemplateNetwork to generate.</typeparam>
public interface INetworkGenerator<TGenerationData>
    where TGenerationData : NetworkData
{
    /// <summary>
    /// Generates a TemplateNetwork with the given data.
    /// </summary>
    /// <param name="generationData">The data used in generating a TemplateNetwork.</param>
    /// <returns>The newly generated TemplateNetwork.</returns>
    TemplateNetwork GenerateNetwork(TGenerationData generationData);
}