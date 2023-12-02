/** \addtogroup DomainTesting
 * @{
 */

using Khartyko.InsigniaCreator.Domain.Data;
using Khartyko.InsigniaCreator.Domain.Generators;
using Khartyko.InsigniaCreator.Domain.Interfaces;
using Khartyko.InsigniaCreator.Domain.NetworkCalculators;
using Khartyko.InsigniaCreator.Domain.NetworkGenerators;
using Khartyko.InsigniaCreator.Domain.Repositories;
using Khartyko.InsigniaCreator.Library.Data;
using Khartyko.InsigniaCreator.Library.Entity;

#pragma warning disable CS8625

namespace Khartyko.InsigniaCreator.Domain.Testing.Repositories;

public class AtlasRepositoryTests
{
    private readonly AtlasData[] _atlasDatas;

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

    private AtlasRepository CreateRepository()
    {
        CartographGenerator cartographGenerator = CreateGenerator();
        var generator = new AtlasGenerator(cartographGenerator);
        var repository = new AtlasRepository(generator);

        return repository;
    }
    
    public AtlasRepositoryTests()
    {
        _atlasDatas = new AtlasData[]
        {
            new()
            {
                Name = "Atlas I",
                Width = 1280,
                Height = 800,
                Background = new RgbColor(255, 255, 255)
            },
            new()
            {
                Name = "Atlas II",
                Width = 1920,
                Height = 1080,
                Background = new RgbColor(255, 255, 255)
            },
            new()
            {
                Name = "Atlas III",
                Width = 2560,
                Height = 1080,
                Background = new RgbColor(255, 255, 255)
            }
        };
    }

    #region Create

    [Fact]
    public void Create_ValidData_Succeeds()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Assert.NotNull(createdAtlas);
    }

    [Fact]
    public void Create_InvalidData_Fails()
    {
        AtlasRepository repository = CreateRepository();

        Assert.Throws<ArgumentNullException>(() => repository.Create(null));
        Assert.Empty(repository.RetrieveAll());
    }

    #endregion Create

    #region Retrieve

    [Fact]
    public void Retrieve_ValidId_Succeeds()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Assert.NotNull(createdAtlas);

        Atlas? retrievedAtlas = repository.Retrieve(createdAtlas.Id);
        Assert.NotNull(retrievedAtlas);
        
        Assert.Equal(createdAtlas, retrievedAtlas);
    }

    [Fact]
    public void Retrieve_InvalidId_EmptyRepository_Fails()
    {
        AtlasRepository repository = CreateRepository();
        
        Assert.Null(repository.Retrieve(0L));
    }

    [Fact]
    public void Retrieve_InvalidId_Fails()
    {
        AtlasRepository repository = CreateRepository();
        
        repository.Create(_atlasDatas[0]);
        
        Assert.Null(repository.Retrieve(2L));
    }

    #endregion Retrieve

    #region RetrieveAll

    [Fact]
    public void RetrieveAll_Empty_Succeeds()
    {
        AtlasRepository repository = CreateRepository();
        
        Assert.Empty(repository.RetrieveAll());
    }

    [Fact]
    public void RetrieveAll_NotEmpty_Succeeds()
    {
        AtlasRepository repository = CreateRepository();
        var cartographs = new List<Atlas>();

        foreach (var data in _atlasDatas)
        {
            Atlas cartograph = repository.Create(data);
            cartographs.Add(cartograph);
        }

        List<Atlas> retrievedAtlases = repository.RetrieveAll()
            .ToList();
        Assert.NotEmpty(retrievedAtlases);

        for (var i = 0; i < _atlasDatas.Length; i++)
        {
            var createdAtlas = cartographs[i];
            var retrievedAtlas = retrievedAtlases[i];
            
            Assert.Equal(createdAtlas, retrievedAtlas);
        }
    }

    #endregion RetrieveAll

    #region Update

    [Fact]
    public void Update_Succeeds()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Atlas updatedAtlas = repository.Create(_atlasDatas[1]);
        
        Assert.True(repository.Update(createdAtlas.Id, updatedAtlas));

        Atlas? updatedInitialAtlas = repository.Retrieve(createdAtlas.Id);
        Assert.NotNull(updatedInitialAtlas);
        
        Assert.Equal(updatedAtlas, updatedInitialAtlas);
    }

    [Fact]
    public void Update_NullAtlas_Fails()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        
        Assert.Throws<ArgumentNullException>(() => repository.Update(createdAtlas.Id, null));

        Atlas? unmodifiedInitialAtlas = repository.Retrieve(createdAtlas.Id);
        Assert.NotNull(unmodifiedInitialAtlas);
    }

    [Fact]
    public void Update_InvalidId_Fails()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Atlas updatedAtlas = repository.Create(_atlasDatas[1]);
        
        Assert.False(repository.Update(4L, updatedAtlas));

        Atlas? unmodifiedInitialAtlas = repository.Retrieve(createdAtlas.Id);
        Assert.NotNull(unmodifiedInitialAtlas);
        
        Assert.Equal(createdAtlas, unmodifiedInitialAtlas);
    }

    #endregion Update

    #region Delete

    [Fact]
    public void Delete_ValidId_Succeeds()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Assert.NotNull(createdAtlas);

        Assert.True(repository.Delete(createdAtlas.Id));
        Assert.Empty(repository.RetrieveAll());
    }

    [Fact]
    public void Delete_InvalidId_EmptyRepository_Fails()
    {
        AtlasRepository repository = CreateRepository();
        
        Assert.False(repository.Delete(0L));
    }

    [Fact]
    public void Delete_InvalidId_Fails()
    {
        AtlasRepository repository = CreateRepository();

        Atlas createdAtlas = repository.Create(_atlasDatas[0]);
        Assert.NotNull(createdAtlas);

        Assert.False(repository.Delete(2L));
    }

    #endregion Delete
}

/** @} */