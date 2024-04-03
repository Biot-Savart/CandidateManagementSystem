using AutoMapper;
using CandidateManagementSystemV2.Server.Data;
using CandidateManagementSystemV2.Server.DTOs;
using CandidateManagementSystemV2.Server.Mappings;
using CandidateManagementSystemV2.Server.Models;
using CandidateManagementSystemV2.Server.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CandidateManagementSystemV2.Server.Tests.ServicesTests
{
    public class CandidateServiceTests
    {
        private readonly IMapper _mapper; // Real instance of AutoMapper
        private readonly DbContextOptions<CandidateDBContext> _dbContextOptions;

        public CandidateServiceTests()
        {
            // Set up AutoMapper with your mapping configuration
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();

            // Use InMemory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<CandidateDBContext>()
                .UseInMemoryDatabase(databaseName: "CandidateDbTest")
                .Options;

            // Seed the database if necessary
            SeedDatabase();
        }

        [Fact]
        public async Task GetAllCandidatesAsync_ReturnsAllNonArchivedCandidates()
        {
            // Arrange
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new CandidateService(context, _mapper);

            // Act
            var result = await service.GetAllCandidatesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0); // Assuming there are non-archived candidates in the seeded database
        }

        [Fact]
        public async Task AddCandidateAsync_AddsCandidateSuccessfully()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new CandidateService(context, _mapper);

            var newCandidateDto = new CandidateDto
            {
                // Populate with necessary data
                CandidateId = 3,
                Name = "New Candidate",
                Email = "mj@test.com",
                Phone = "0236565698"
            };

            var addedCandidateDto = await service.AddCandidateAsync(newCandidateDto);

            Assert.NotNull(addedCandidateDto);
            Assert.NotEqual(default, addedCandidateDto.CandidateId); // Assuming CandidateId is auto-generated
            Assert.Equal("New Candidate", addedCandidateDto.Name);
            // Verify that the candidate is actually added to the database
            var candidateExists = context.Candidates.Any(c => c.CandidateId == addedCandidateDto.CandidateId);
            Assert.True(candidateExists);
        }

        [Fact]
        public async Task GetCandidateByIdAsync_ReturnsCandidate_WhenCandidateExists()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new CandidateService(context, _mapper);

            // Assuming there's already a candidate with ID 1 seeded in the database
            var candidateDto = await service.GetCandidateByIdAsync(1);

            Assert.NotNull(candidateDto);
            Assert.Equal(1, candidateDto.CandidateId);
            // Add additional assertions as needed based on your CandidateDto structure
        }

        [Fact]
        public async Task DeleteCandidateAsync_ArchivesCandidateSuccessfully()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            var service = new CandidateService(context, _mapper);

            // Assuming there's already a candidate with ID 1 seeded in the database
            await service.DeleteCandidateAsync(2);

            // Refresh the context to ensure the change is reflecteds
            context.Entry(context.Candidates.Find(1)).Reload();
            var candidate = await context.Candidates.FindAsync(2);

            Assert.NotNull(candidate.Archived); // Ensure the candidate is marked as archived
        }

        private void SeedDatabase()
        {
            using var context = new CandidateDBContext(_dbContextOptions);
            if (!context.Candidates.Any())
            {
                context.Candidates.Add(new Candidate {CandidateId = 1,  Name = "Mary Jane", Archived = null, Email="mj@test.com", Phone = "0236565698" }); // Add more candidates as needed
                context.Candidates.Add(new Candidate { CandidateId = 2, Name = "Frank Otte", Archived = null, Email = "fo@test.com", Phone = "0236565698" }); // Add more candidates as needed
                context.SaveChanges();
            }
        }
    }
}
