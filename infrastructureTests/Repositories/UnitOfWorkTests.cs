using infrastructure.Data;
using infrastructure.Repositories;
using infrastructureTests.TestUtils;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace infrastructureTests.Repositories;

[TestFixture]
public class UnitOfWorkTests
{
    private UnitOfWork _unitOfWork;
    private ApplicationDbContext _context;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("fakeDb")
                .Options;

        _context = Substitute.For<ApplicationDbContext>(options);

        _unitOfWork = new UnitOfWork(_context);
    }

    [Test]
    public async Task Complete_GivenUnitOfWork_ShouldCallSaveChangesAsync()
    {
        // Given When
        await _unitOfWork.CompleteAsync();

        // Then
        await _context.Received(1).SaveChangesAsync();
    }

    [Test]
    public void Repository_GivenUnitOfWork_ShouldReturnRepositoryInstance_WhenNotExists()
    {
        // When
        var actualRepository = _unitOfWork.Repository<SomeEntity>();

        // Then
        Assert.That(actualRepository, Is.InstanceOf<GenericRepository<SomeEntity>>());
    }

    [Test]
    public void Repository_GivenUnitOfWork_ShouldReturnRepositoryInstance_WhenExists()
    {
        // When
        _unitOfWork.Repository<SomeEntity>();

        // Then
        Assert.That(_unitOfWork.Repository<SomeEntity>(), Is.InstanceOf<GenericRepository<SomeEntity>>());
    }
}