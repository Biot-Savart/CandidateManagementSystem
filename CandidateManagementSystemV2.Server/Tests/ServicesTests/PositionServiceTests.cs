using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.Mappings;
using CandidateManagementSystemV2.Server.Models;
using CandidateManagementSystemV2.Server.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CandidateManagementSystemV2.Server.Tests.ServicesTests
{
    public class PositionServiceTests
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<CandidateDBContext> _dbContextOptions;

        public PositionServiceTests()
        {
            // AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();

            // InMemory database setup
            _dbContextOptions = new DbContextOptionsBuilder<CandidateDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the database with test data
            SeedDatabase();
        }

        [Fact]
        public async Task GetAllPositionsAsync_ReturnsAllPositions()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new PositionService(context, _mapper);

            var results = await service.GetAllPositionsAsync();

            Assert.NotNull(results);
            Assert.True(results.Any()); // Assuming there's at least one non-archived position seeded
        }

        [Fact]
        public async Task GetPositionByIdAsync_ReturnsPosition_WhenPositionExists()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new PositionService(context, _mapper);

            // Assuming a position with ID 1 exists in the seeded data
            var result = await service.GetCPositionByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PositionId);
        }

        [Fact]
        public async Task GetPositionByIdAsync_ThrowsKeyNotFoundException_WhenPositionDoesNotExist()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new PositionService(context, _mapper);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetCPositionByIdAsync(999));
        }

        private void SeedDatabase()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            if (!context.Positions.Any())
            {
                context.Positions.Add(new Position
                {
                    // Populate with test data
                    PositionId = 1,
                    Title = "Test Position",
                    Description = "Description",
                });
                context.SaveChanges();
            }
        }
    }
}
