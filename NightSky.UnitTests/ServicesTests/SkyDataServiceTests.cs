using System.Linq.Expressions;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using NightSky.API.DAL;
using NightSky.API.DAL.Entities;
using NightSky.API.Exceptions;
using NightSky.API.Models;
using NightSky.API.Services;

namespace NightSky.UnitTests.ServicesTests;

public class SkyDataServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ApplicationDbContext> _contextMock;
    private readonly SkyDataService _skyDataService;

    public SkyDataServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _contextMock = new Mock<ApplicationDbContext>();
        _skyDataService = new SkyDataService(_mapperMock.Object, _contextMock.Object);
    }

    [Fact]
    public async Task AddSkyData_WithValidModel_ShouldAddEntityToContextAndReturnSkyId()
    {
        // Arrange
        var model = new SkyDataModel();
        var entity = new SkyData { SkyId = 1 };
        _mapperMock.Setup(m => m.Map<SkyData>(model)).Returns(entity);
        _contextMock.Setup(c => c.SkyData).ReturnsDbSet(new List<SkyData>() { entity });

        // Act
        var result = await _skyDataService.AddSkyData(model);

        // Assert
        _contextMock.Verify(c => c.SaveChangesAsync(new CancellationToken()), Times.Once);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task AddSkyData_WithExistingSkyId_ShouldThrowBaseNightSkyException()
    {
        // Arrange
        var model = new SkyDataModel { SkyId = 1 };

        // Act & Assert
        await Assert.ThrowsAsync<BaseNightSkyException>(() => _skyDataService.AddSkyData(model));
    }

    [Fact]
    public async Task AddSkyData_WithExistingDataDate_ShouldThrowBaseNightSkyException()
    {
        // Arrange
        var date = DateTime.Now;
        var model = new SkyDataModel { DataDate = date };
        var entity = new SkyData { SkyId = 1, DataDate = date };
        _contextMock.Setup(c => c.SkyData).ReturnsDbSet(new List<SkyData>() { entity });

        // Act & Assert
        await Assert.ThrowsAsync<BaseNightSkyException>(() => _skyDataService.AddSkyData(model));
    }

    [Fact]
    public async Task UpdateSkyData_ShouldUpdateEntityAndSaveChanges()
    {
        // Arrange
        var model = new SkyDataModel();
        var existingEntity = new SkyData();
        _contextMock.Setup(c => c.SkyData)
            .ReturnsDbSet(new List<SkyData>() { existingEntity });

        // Act
        await _skyDataService.UpdateSkyData(model);

        // Assert
        _mapperMock.Verify(m => m.Map(model, existingEntity), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_WithValidId_ShouldReturnSkyDataModel()
    {
        // Arrange
        var id = 1;
        var existingEntity = new SkyData();
        _contextMock.Setup(c => c.SkyData.FindAsync(id)).ReturnsAsync(existingEntity);
        var mappedModel = new SkyDataModel();
        _mapperMock.Setup(m => m.Map<SkyDataModel>(existingEntity)).Returns(mappedModel);

        // Act
        var result = await _skyDataService.GetById(id);

        // Assert
        Assert.Equal(mappedModel, result);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ShouldThrowBaseNightSkyException()
    {
        // Arrange
        int? id = null;

        // Act & Assert
        await Assert.ThrowsAsync<BaseNightSkyException>(() => _skyDataService.GetById(id.Value));
    }

    [Fact]
    public async Task GetById_WithNonExistentEntity_ShouldThrowSkyDataDoesNotExistException()
    {
        // Arrange
        var id = 1;
        _contextMock.Setup(c => c.SkyData.FindAsync(id)).ReturnsAsync((SkyData)null);

        // Act & Assert
        await Assert.ThrowsAsync<SkyDataDoesNotExistException>(() => _skyDataService.GetById(id));
    }

    [Fact]
    public async Task GetDatesRange_ShouldReturnCollectionOfSkyDataModels()
    {
        // Arrange
        var startDate = DateTime.Now.Date;
        var endDate = startDate.AddDays(1);
        var entities = new List<SkyData> { new SkyData(), new SkyData() };
        _contextMock.Setup(c => c.SkyData.Where(It.IsAny<Expression<Func<SkyData, bool>>>()))
            .Returns(entities.AsQueryable());
        var mappedModels = new List<SkyDataModel> { new SkyDataModel(), new SkyDataModel() };
        _mapperMock.Setup(m => m.Map<SkyDataModel>(It.IsAny<SkyData>())).Returns(mappedModels[0])
            .Returns(mappedModels[1]);

        // Act
        var result = await _skyDataService.GetDatesRange(startDate, endDate);

        // Assert
        Assert.Equal(mappedModels, result);
    }
}