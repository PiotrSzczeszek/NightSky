using System.Collections;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NightSky.API.DAL;
using NightSky.API.DAL.Entities;
using NightSky.API.Exceptions;
using NightSky.API.Models;
using NightSky.API.Services;

namespace NightSky.UnitTests.ServicesTests;

public class ConstellationServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ApplicationDbContext> _contextMock;

    private readonly ConstellationService _sut;

    public ConstellationServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        Mock<IStarsToConstellationsService> starsToConstellationsServiceMock = new();

        _contextMock = new Mock<ApplicationDbContext>();

        _sut = new ConstellationService(_contextMock.Object, _mapperMock.Object,
            starsToConstellationsServiceMock.Object);
    }

    [Fact]
    public async Task AddConstellation_GivenValidModel_ReturnsId()
    {
        var model = new ConstellationModel() { Stars = new List<StarModel>() };
        var entity = new Constellation() { ConstallationId = 1 };

        _mapperMock.Setup(mapper => mapper.Map<ConstellationModel, Constellation>(model,
                It.IsAny<Action<IMappingOperationOptions<ConstellationModel, Constellation>>>()))
            .Returns(entity);

        var result = await _sut.AddConstellation(model);

        Assert.Equal(entity.ConstallationId, result);
    }

    [Fact]
    public async Task AddConstellation_GivenModelWithId_ThrowsException()
    {
        var model = new ConstellationModel() { ConstallationId = 1, Stars = new List<StarModel>() };

        await Assert.ThrowsAsync<BaseNightSkyException>(() => _sut.AddConstellation(model));
    }


    [Theory]
    [ClassData(typeof(ConstellationTestData))]
    public async Task UpdateConstellation_GivenValidModel_UpdatesEntity(Constellation resultEntity, Constellation existingEntity, ConstellationModel model)
    {
        // var model = new ConstellationModel() { ConstallationId = 1 };
        // var entity = new Constellation() { ConstallationId = 1 };

        var entities = new List<Constellation>() { existingEntity };
        _contextMock.Setup(x => x.Constellations).ReturnsDbSet(entities);
        
        await _sut.UpdateConstellation(model);
        //var entityAfterUpdate = await _sut.GetById(existingEntity.ConstallationId);

        _mapperMock.Verify(
            mapper => mapper.Map(model, existingEntity,
                It.IsAny<Action<IMappingOperationOptions<ConstellationModel, Constellation>>>()), Times.Once);
        _mapperMock.Verify();
        Assert.Equivalent(resultEntity, existingEntity);
    }

    [Fact]
    public async Task DeleteConstellation_GivenValidId_DeletesEntity()
    {
        var id = 1;
        var entity = new Constellation() { ConstallationId = 1 };

        var entities = new List<Constellation>() { entity };
        _contextMock.Setup(x => x.Constellations).ReturnsDbSet(entities);

        await _sut.DeleteConstellation(id);

        _contextMock.Verify(context => context.Remove(entity), Times.Once);
    }

    [Fact]
    public async Task GetById_GivenValidId_ReturnsModel()
    {
        var id = 1;
        var entity = new Constellation() { ConstallationId = 1 };
        var model = new ConstellationModel() { ConstallationId = 1 };

        var entities = new List<Constellation>() { entity };
        _contextMock.Setup(x => x.Constellations).ReturnsDbSet(entities);
        _mapperMock.Setup(mapper => mapper.Map<ConstellationModel>(entity))
            .Returns(model);

        var result = await _sut.GetById(id);

        Assert.Equal(model, result);
    }
}

public class ConstellationTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new Constellation() { ConstallationId = 1 },
            new Constellation() { ConstallationId = 1 },
            new ConstellationModel() { ConstallationId = 1 }
        };
        yield return new object[]
        {
            new Constellation() { ConstallationId = 2, Stars = new List<Star>() },
            new Constellation() { ConstallationId = 2, Stars = new List<Star>() },
            new ConstellationModel() { ConstallationId = 2, Stars = new List<StarModel>() }
        };
        yield return new object[]
        {
            new Constellation()
                { ConstallationId = 3, Stars = new List<Star>() { new() { StarId = 1 }, new() { StarId = 2 } } },
            new Constellation()
                { ConstallationId = 3, Stars = new List<Star>() { new() { StarId = 1 }, new() { StarId = 2 } } },
            new ConstellationModel()
            {
                ConstallationId = 3, Stars = new List<StarModel>()
                {
                    new() { StarId = 1 },
                    new() { StarId = 2 },
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}