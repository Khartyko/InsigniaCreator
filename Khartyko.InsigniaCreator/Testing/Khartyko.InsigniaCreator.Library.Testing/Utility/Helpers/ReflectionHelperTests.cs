/** \addtogroup LibraryTests
 * @{
 */
using System.Reflection;
using Khartyko.InsigniaCreator.Library.Utility;
using Khartyko.InsigniaCreator.Library.Utility.Helpers;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing.Utility.Helpers;

public class ReflectionHelperTests
{
    private object _testValue = new();
    
    private ReflectionMetadata? TestMetadata { get; set; }

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

    internal class IndexingTestClass
    {
        internal ReflectionMetadata? Metadata { get; set; }
        private object _objValue = new object();
        
        internal object this[object obj]
        {
            get
            {
                Metadata = ReflectionHelper.GetCallerMetadata();
                
                return _objValue;
            }
            set
            {
                Metadata = ReflectionHelper.GetCallerMetadata();

                _objValue = value;
            }
        }
    }

    internal class ConstructorMetadataTestClass
    {
        internal ReflectionMetadata Metadata { get; set; }
        
        public ConstructorMetadataTestClass()
        {
            Metadata = ReflectionHelper.GetCallerMetadata();
        }
    }

    #region GetCallerMetadata

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
        
        Assert.NotNull(TestMetadata);
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
        
        Assert.NotNull(TestMetadata);
        MethodBase methodInfo = GetType().GetMethod(TestMetadata.MethodBase.Name)!;
        
        Assert.Equal(GetType(), TestMetadata.Type);
        Assert.Null(methodInfo);
        Assert.Equal(MethodTypes.PropertySet, TestMetadata.MethodType);
        Assert.Equal($"set_{nameof(TestProperty)}", TestMetadata.MethodBase.Name);
    }

    [Fact]
    public void GetCallerMetadata_IndexerGet_Succeeds()
    {
        var testClass = new IndexingTestClass();

        _ = testClass[testClass];

        Assert.NotNull(testClass.Metadata);
        Assert.Equal(testClass.GetType(), testClass.Metadata.Type);
        Assert.Equal(MethodTypes.IndexerGet, testClass.Metadata.MethodType);
        Assert.Equal("get_Item", testClass.Metadata.MethodBase.Name);
    }

    [Fact]
    public void GetCallerMetadata_IndexerSet_Succeeds()
    {
        var testClass = new IndexingTestClass();

        testClass[testClass] = new object();

        Assert.NotNull(testClass.Metadata);
        Assert.Equal(testClass.GetType(), testClass.Metadata.Type);
        Assert.Equal(MethodTypes.IndexerSet, testClass.Metadata.MethodType);
        Assert.Equal("set_Item", testClass.Metadata.MethodBase.Name);
    }


    [Fact]
    public void GetCallerMetadata_Constructor_Succeeds()
    {
        var testClass = new ConstructorMetadataTestClass();

        ReflectionMetadata metadata = testClass.Metadata;
        
        Assert.NotNull(testClass.Metadata);
        Assert.Equal(testClass.GetType(), metadata.Type);
        Assert.Equal(MethodTypes.Constructor, metadata.MethodType);
        Assert.Equal(".ctor", metadata.MethodBase.Name);
    }
    
    [Fact]
    public void GetCallerMetadata_Lambda_Succeeds()
    {
        ReflectionMetadata? metadata = null;

        // ReSharper disable once ConvertToLocalFunction
        Action action = () =>
        {
            metadata = ReflectionHelper.GetCallerMetadata();
        };
        
        action();
        
        Assert.NotNull(metadata);
        Assert.Equal(MethodTypes.Lambda, metadata.MethodType);
    }

    [Fact]
    public void GetCallerMetadata_InvalidFrameOffset_Fails()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ReflectionHelper.GetCallerMetadata(-1));
    }
    
    #endregion GetCallerMetadata
    
    #region ConstructMethodSignature

    [Fact]
    public void ConstructMethodSignature_Succeeds()
    {
        ReflectionMetadata metadata = ReflectionHelper.GetCallerMetadata();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(metadata);

        const string expectedSignature = $"ReflectionHelperTests::ConstructMethodSignature_Succeeds()";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Fact]
    public void ConstructMethodSignature_NullMetadata_Fails()
    {
        ReflectionMetadata nullMetadata = null;

        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.ConstructMethodSignature(nullMetadata));
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

    [Theory]
    [InlineData(null, "value")]
    [InlineData("val", "value")]
    [InlineData("value", ">value<")]
    public void ConstructMethodSignature_SetProperty_Succeeds(string? parameterName, string expectedValueName)
    {
        TestProperty = new object();

        string methodSignature = ReflectionHelper.ConstructMethodSignature(TestMetadata, parameterName);
        
        var expectedSignature = $"{GetType().Name}::{nameof(TestProperty)} = {expectedValueName}";
		
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Theory]
    [InlineData(null, "obj")]
    [InlineData("ob", "obj")]
    [InlineData("obj", ">obj<")]
    public void ConstructMethodSignature_GetIndexer_Succeeds(string? parameterName, string expectedNotedParameter)
    {
        var testClass = new IndexingTestClass();

        _ = testClass[testClass];

        string methodSignature = ReflectionHelper.ConstructMethodSignature(testClass.Metadata, parameterName);

        var expectedSignature = $"{nameof(IndexingTestClass)}[{expectedNotedParameter}]";
        
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Theory]
    [InlineData(null, "obj")]
    [InlineData("ob", "obj")]
    [InlineData("obj", ">obj<")]
    public void ConstructMethodSignature_SetIndexer_IndexValue_Succeeds(string? parameterName, string expectedNotedParameter)
    {
        var testClass = new IndexingTestClass();

        testClass[testClass] = testClass;

        string methodSignature = ReflectionHelper.ConstructMethodSignature(testClass.Metadata, parameterName);

        var expectedSignature = $"{nameof(IndexingTestClass)}[{expectedNotedParameter}] = value";
        
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Theory]
    [InlineData(null, "value")]
    [InlineData("va", "value")]
    [InlineData("value", ">value<")]
    public void ConstructMethodSignature_SetIndexer_RighthandValue_Succeeds(string? parameterName, string expectedNotedValue)
    {
        var testClass = new IndexingTestClass();

        testClass[testClass] = testClass;

        string methodSignature = ReflectionHelper.ConstructMethodSignature(testClass.Metadata, parameterName);

        var expectedSignature = $"{nameof(IndexingTestClass)}[obj] = {expectedNotedValue}";
        
        Assert.Equal(expectedSignature, methodSignature);
    }

    [Fact]
    public void ConstructMethodSignature_Constructor_Succeeds()
    {
        var testClass = new ConstructorMetadataTestClass();

        string signature = ReflectionHelper.ConstructMethodSignature(testClass.Metadata);

        var expectedSignature = $"{nameof(ConstructorMetadataTestClass)}::{nameof(ConstructorMetadataTestClass)}()";
        
        Assert.Equal(expectedSignature, signature);
    }

    [Fact]
    public void ConstructMethodSignature_LambdaMethod_Succeeds()
    {
        ReflectionMetadata? metadata = null;

        // ReSharper disable once ConvertToLocalFunction
        Action action = () =>
        {
            metadata = ReflectionHelper.GetCallerMetadata();
        };
        
        action();
        
        string signature = ReflectionHelper.ConstructMethodSignature(metadata);
        
        Assert.Equal("ReflectionHelperTests::ConstructMethodSignature_LambdaMethod_Succeeds() -> λ()", signature);
    }
    
    #endregion ConstructMethodSignature
}
/** @} */