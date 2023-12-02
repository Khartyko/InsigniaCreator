/** \addtogroup DomainTesting
* @{
*/

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Domain.Repositories;
using Khartyko.InsigniaCreator.Library.Entity;
using Khartyko.InsigniaCreator.TestingLibrary;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.Repositories;

public class CartographRepositoryTests
{
    private readonly CartographData[] _cartographDatas;

    private CartographGenerator CreateGenerator()
    {
        var triangularCalculator = new TriangularNetworkCalculator();
        var squareCalculator = new SquareNetworkCalculator();
        var hexagonalCalculator = new HexagonalNetworkCalculator();

        INetworkGenerator<TriangularNetworkData> triNetworkGenerator = new TriangularNetworkGenerator(triangularCalculator);
        INetworkGenerator<NetworkData> quadNetworkGenerator = new SquareNetworkGenerator(squareCalculator);
        INetworkGenerator<HexagonalNetworkData> hexNetworkGenerator = new HexagonalNetworkGenerator(hexagonalCalculator);

        return new CartographGenerator(
            triNetworkGenerator,
            quadNetworkGenerator,
            hexNetworkGenerator
        );
    }

    private CartographRepository CreateRepository()
    {
        var generator = CreateGenerator();
        var repository = new CartographRepository(generator);

        return repository;
    }
    
    public CartographRepositoryTests()
    {
        _cartographDatas = new CartographData[]
        {
            new()
            {
                AtlasId = 1L,
                Name = "Cartograph I",
                NetworkData = DataGenerator.GenerateSquareNetworkData()
            },
            new()
            {
                AtlasId = 2L,
                Name = "Cartograph II",
                NetworkData = DataGenerator.GenerateSquareNetworkData()
            },
            new()
            {
                AtlasId = 3L,
                Name = "Cartograph III",
                NetworkData = DataGenerator.GenerateSquareNetworkData()
            }
        };
    }

    #region Create

    [Fact]
    public void Create_ValidData_Succeeds()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Assert.NotNull(createdCartograph);
    }

    [Fact]
    public void Create_InvalidData_Fails()
    {
        CartographRepository repository = CreateRepository();

        Assert.Throws<ArgumentNullException>(() => repository.Create(null));
        Assert.Empty(repository.RetrieveAll());
    }

    #endregion Create

    #region Retrieve

    [Fact]
    public void Retrieve_ValidId_Succeeds()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Assert.NotNull(createdCartograph);

        Cartograph? retrievedCartograph = repository.Retrieve(createdCartograph.Id);
        Assert.NotNull(retrievedCartograph);
        
        Assert.Equal(createdCartograph, retrievedCartograph);
    }

    [Fact]
    public void Retrieve_InvalidId_EmptyRepository_Fails()
    {
        CartographRepository repository = CreateRepository();
        
        Assert.Null(repository.Retrieve(0L));
    }

    [Fact]
    public void Retrieve_InvalidId_Fails()
    {
        CartographRepository repository = CreateRepository();
        
        repository.Create(_cartographDatas[0]);
        
        Assert.Null(repository.Retrieve(2L));
    }

    #endregion Retrieve

    #region RetrieveAll

    [Fact]
    public void RetrieveAll_Empty_Succeeds()
    {
        CartographRepository repository = CreateRepository();
        
        Assert.Empty(repository.RetrieveAll());
    }

    [Fact]
    public void RetrieveAll_NotEmpty_Succeeds()
    {
        CartographRepository repository = CreateRepository();
        var cartographs = new List<Cartograph>();

        foreach (var data in _cartographDatas)
        {
            Cartograph cartograph = repository.Create(data);
            cartographs.Add(cartograph);
        }

        List<Cartograph> retrievedCartographs = repository.RetrieveAll()
            .ToList();
        Assert.NotEmpty(retrievedCartographs);

        for (var i = 0; i < _cartographDatas.Length; i++)
        {
            var createdCartograph = cartographs[i];
            var retrievedCartograph = retrievedCartographs[i];
            
            Assert.Equal(createdCartograph, retrievedCartograph);
        }
    }

    #endregion RetrieveAll

    #region Update

    [Fact]
    public void Update_Succeeds()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Cartograph updatedCartograph = repository.Create(_cartographDatas[1]);
        
        Assert.True(repository.Update(createdCartograph.Id, updatedCartograph));

        Cartograph? updatedInitialCartograph = repository.Retrieve(createdCartograph.Id);
        Assert.NotNull(updatedInitialCartograph);
        
        Assert.Equal(updatedCartograph, updatedInitialCartograph);
    }

    [Fact]
    public void Update_NullCartograph_Fails()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        
        Assert.Throws<ArgumentNullException>(() => repository.Update(createdCartograph.Id, null));

        Cartograph? unmodifiedInitialCartograph = repository.Retrieve(createdCartograph.Id);
        Assert.NotNull(unmodifiedInitialCartograph);
    }

    [Fact]
    public void Update_InvalidId_Fails()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Cartograph updatedCartograph = repository.Create(_cartographDatas[1]);
        
        Assert.False(repository.Update(4L, updatedCartograph));

        Cartograph? unmodifiedInitialCartograph = repository.Retrieve(createdCartograph.Id);
        Assert.NotNull(unmodifiedInitialCartograph);
        
        Assert.Equal(createdCartograph, unmodifiedInitialCartograph);
    }

    #endregion Update

    #region Delete

    [Fact]
    public void Delete_ValidId_Succeeds()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Assert.NotNull(createdCartograph);

        Assert.True(repository.Delete(createdCartograph.Id));
        Assert.Empty(repository.RetrieveAll());
    }

    [Fact]
    public void Delete_InvalidId_EmptyRepository_Fails()
    {
        CartographRepository repository = CreateRepository();
        
        Assert.False(repository.Delete(0L));
    }

    [Fact]
    public void Delete_InvalidId_Fails()
    {
        CartographRepository repository = CreateRepository();

        Cartograph createdCartograph = repository.Create(_cartographDatas[0]);
        Assert.NotNull(createdCartograph);

        Assert.False(repository.Delete(2L));
    }

    #endregion Delete
}

/** @} */