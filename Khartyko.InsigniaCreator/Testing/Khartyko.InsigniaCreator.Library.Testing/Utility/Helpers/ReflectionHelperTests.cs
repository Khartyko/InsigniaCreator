using System.Reflection;
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ReflectionHelperTests
{
    private object _testValue = new();
    
    private ReflectionMetadata TestMetadata { get; set; }

    private object TestProperty
    {
        get
        {
            TestMetadata = ReflectionHelper.GetCallerMetadata();

            return _testValue;
        }
        set
        {
            TestMetadata = ReflectionHelper.GetCallerMetadata();
            
            _testValue = value;
        }
    }
    
    [Fact]
    public void GetCallerMetadata_RegularMethod_Succeeds()
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        MethodBase methodInfo = GetType().GetMethod(nameof(GetCallerMetadata_RegularMethod_Succeeds))!;

        Assert.Equal(GetType(), metadata.Type);
        Assert.Equal(methodInfo, metadata.MethodBase);
        Assert.Equal(MethodTypes.RegularMethod, metadata.MethodType);
    }

    [Fact]
    public void GetCallerMetadata_PropertyGet_Succeeds()
    {
        _ = TestProperty;
        
        MethodBase methodInfo = GetType().GetMethod(TestMetadata.MethodBase.Name)!;
        
        Assert.Equal(GetType(), TestMetadata.Type);
        Assert.Null(methodInfo);
        Assert.Equal(MethodTypes.PropertyGet, TestMetadata.MethodType);
        Assert.Equal($"get_{nameof(TestProperty)}", TestMetadata.MethodBase.Name);
    }

    [Fact]
    public void GetCallerMetadata_PropertySet_Succeeds()
    {
        TestProperty = new object();
        
        MethodBase methodInfo = GetType().GetMethod(TestMetadata.MethodBase.Name)!;
        
        Assert.Equal(GetType(), TestMetadata.Type);
        Assert.Null(methodInfo);
        Assert.Equal(MethodTypes.PropertySet, TestMetadata.MethodType);
        Assert.Equal($"set_{nameof(TestProperty)}", TestMetadata.MethodBase.Name);
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
    public void ConstructMethodSignature_GetProperty_Succeeds()
    {
        _ = TestProperty;

        string methodSignature = ReflectionHelper.ConstructMethodSignature(TestMetadata);
        
        var expectedSignature = $"{GetType().Name}::{nameof(TestProperty)}";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Fact]
    public void ConstructMethodSignature_SetProperty_Succeeds()
    {
        TestProperty = new object();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(TestMetadata);
        
        var expectedSignature = $"{GetType().Name}::{nameof(TestProperty)}";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Fact]
    public void ConstructMethodSignature_NullMetadata_Fails()
    {
        ReflectionMetadata nullMetadata = null;

        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.ConstructMethodSignature(nullMetadata));
    }
}