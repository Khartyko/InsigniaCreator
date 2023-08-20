using Khartyko.InsigniaCreator.Library.Interfaces;
using Moq;

#pragma warning disable CS8600, CS8604

namespace Khartyko.InsigniaCreator.Library.Testing;

public sealed class ArchonTests
{
    #region Archivist

    [Fact]
    public void Archivist_Get_Succeeds()
    {
        var mockedArchivist = new Mock<IArchivist>();
        
        Archon.Initialize(mockedArchivist.Object);
        
        Assert.NotNull(Archon.Archivist);

        Archon.Shutdown();
    }

    [Fact]
    public void Archivist_Get_Fails()
    {
        Assert.Null(Archon.Archivist);
    }
    
    #endregion Archivist

    #region Initialize

    [Fact]
    public void Initialize_Succeeds()
    {
        var mockedArchivist = new Mock<IArchivist>();
        
        Archon.Initialize(mockedArchivist.Object);
        
        Assert.Equal(mockedArchivist.Object, Archon.Archivist);
        
        Archon.Shutdown();
    }

    [Fact]
    public void Initialize_NullArgument_Fails()
    {
        IArchivist nullArchivist = null;

        Assert.Throws<ArgumentNullException>(() => Archon.Initialize(nullArchivist));
        Assert.Null(Archon.Archivist);
    }

    [Fact]
    public void Initialize_ArchivistAlreadySet_Fails()
    {
        var initialMockedArchivist = new Mock<IArchivist>();
        var extraMockedArchivist = new Mock<IArchivist>();
        
        Archon.Initialize(initialMockedArchivist.Object);

        Assert.Throws<ArgumentException>(() => Archon.Initialize(extraMockedArchivist.Object));
        Assert.NotEqual(extraMockedArchivist.Object, Archon.Archivist);
        
        Archon.Shutdown();
    }

    #endregion Initialize

    #region Shutdown

    [Fact]
    public void Shutdown_Succeeds()
    {
        var mockedArchivist = new Mock<IArchivist>();
        
        Archon.Initialize(mockedArchivist.Object);
        
        Assert.NotNull(Archon.Archivist);

        Archon.Shutdown();
        
        Assert.Null(Archon.Archivist);
    }

    [Fact]
    public void Shutdown_Uninitialized_Fails()
    {
        Assert.Throws<ApplicationException>(Archon.Shutdown);
    }

    #endregion Shutdown
}