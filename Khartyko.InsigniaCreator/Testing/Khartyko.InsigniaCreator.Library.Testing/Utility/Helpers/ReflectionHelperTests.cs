using System.Reflection;
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ReflectionHelperTests
{
    [Fact]
    public void MetaDataTest()
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        MethodBase methodInfo = GetType().GetMethod(nameof(MetaDataTest))!;

        Assert.Equal(GetType(), metadata.Type);
        Assert.Equal(methodInfo, metadata.MethodBase);
    }

    [Fact]
    public void ConstructMethodSignature_Succeeds()
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(metadata);

        const string expectedSignature = $"ReflectionHelperTests::ConstructMethodSignature_Succeeds()";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Fact]
    public void ConstructMethodSignature_EmptyParameterName_Succeeds()
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(metadata, string.Empty);

        const string expectedSignature = $"ReflectionHelperTests::ConstructMethodSignature_EmptyParameterName_Succeeds()";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Theory]
    [InlineData("parameterName")]
    public void ConstructMethodSignature_ParameterName_Succeeds(string parameterName)
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(metadata, parameterName);

        var expectedSignature = $"{GetType().Name}::{nameof(ConstructMethodSignature_ParameterName_Succeeds)}(>{parameterName}<)";
		
        Assert.Equal(expectedSignature, methodSignature);
    }
    
    [Fact]
    public void ConstructMethodSignature_NullMetadata_Fails()
    {
        ReflectionMetadata nullMetadata = null;

        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.ConstructMethodSignature(nullMetadata));
    }
}