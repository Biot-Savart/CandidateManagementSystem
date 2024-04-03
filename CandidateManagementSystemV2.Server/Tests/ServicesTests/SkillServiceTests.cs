using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.Mappings;
using CandidateManagementSystemV2.Server.Models;
using CandidateManagementSystemV2.Server.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CandidateManagementSystemV2.Server.Tests.ServicesTests
{
    public class SkillServiceTests
    {
        private readonly IMapper _mapper; // AutoMapper instance
        private readonly DbContextOptions<CandidateDBContext> _dbContextOptions;

        public SkillServiceTests()
        {
            // AutoMapper configuration
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile()); // Assuming you have defined your mappings here
            });
            _mapper = mapperConfiguration.CreateMapper();

            // InMemory database setup
            _dbContextOptions = new DbContextOptionsBuilder<CandidateDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseForSkills")
                .Options;

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            using (var context = new CandidateDBContext(_dbContextOptions))
            {
                if (!context.Skills.Any())
                {
                    context.Skills.AddRange(
                        new Skill { SkillId = 1, Name = "C#", Archived = null },
                        new Skill { SkillId = 2, Name = "JavaScript", Archived = null }
                    // Add more skills as needed for testing
                    );
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task GetAllSkillsAsync_ReturnsAllNonArchivedSkills()
        {
            using (var context = new CandidateDBContext(_dbContextOptions))
            {
                var service = new SkillService(context, _mapper);
                var result = await service.GetAllSkillsAsync();

                Assert.NotNull(result);
                Assert.True(result.Any());
            }
        }

        [Fact]
        public async Task GetSkillCandidateCount_ReturnsCorrectCounts()
        {
            using (var context = new CandidateDBContext(_dbContextOptions))
            {
                var service = new SkillService(context, _mapper);
                var result = await service.GetSkillCandidateCount();

                Assert.NotNull(result);
                // Further assertions depending on how you've seeded your test data
            }
        }

        [Fact]
        public async Task GetSkillByIdAsync_ReturnsSkill_WhenSkillExists()
        {
            using (var context = new CandidateDBContext(_dbContextOptions))
            {
                var service = new SkillService(context, _mapper);
                var result = await service.GetSkillByIdAsync(1); // Assuming a skill with ID 1 exists

                Assert.NotNull(result);
                Assert.Equal(1, result.SkillId);
                Assert.Equal("C#", result.Name);
            }
        }

        [Fact]
        public async Task GetSkillByIdAsync_ThrowsKeyNotFoundException_WhenSkillDoesNotExist()
        {
            using (var context = new CandidateDBContext(_dbContextOptions))
            {
                var service = new SkillService(context, _mapper);
                await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetSkillByIdAsync(999));
            }
        }
    }
}
