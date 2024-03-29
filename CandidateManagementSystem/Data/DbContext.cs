using CandidateManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Data
{
    public class CandidateDBContext: DbContext
    {

        public CandidateDBContext(DbContextOptions<CandidateDBContext> options) : base(options) {}

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<CandidatePosition> CandidatePositions { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CandidatePosition>()
                .HasKey(pc => new { pc.CandidateId, pc.PositionId });

            modelBuilder.Entity<CandidatePosition>()
                .HasOne(pc => pc.Candidate)
                .WithMany(c => c.CandidatePositions)
                .HasForeignKey(pc => pc.CandidateId);

            modelBuilder.Entity<CandidatePosition>()
                .HasOne(pc => pc.Position)
                .WithMany(p => p.CandidatePositions)
                .HasForeignKey(pc => pc.PositionId);

            // Seed Positions
            modelBuilder.Entity<Position>().HasData(
                new Position { PositionId = 1, Title = "Software Developer", Description = "Develops software.", Created = DateTime.UtcNow },
                new Position { PositionId = 2, Title = "Data Scientist", Description = "Works with data.", Created = DateTime.UtcNow }
            );

            // Seed Candidates
            modelBuilder.Entity<Candidate>().HasData(
                new Candidate { CandidateId = 1, Name = "John Doe", Email = "john.doe@example.com", Phone = "1234567890", Created = DateTime.UtcNow, Experience = 5 },
                new Candidate { CandidateId = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Phone = "0987654321", Created = DateTime.UtcNow, Experience = 1 }
            );

            // Seed PositionCandidates (Many-to-Many relationship)
            modelBuilder.Entity<CandidatePosition>().HasData(
                new CandidatePosition { CandidateId = 1, PositionId = 1 },
                new CandidatePosition { CandidateId = 2, PositionId = 2 }
            );

            modelBuilder.Entity<Skill>().HasData(
               new Skill { SkillId = 1, Name = "C#", YearsOfExperience = 5, Created = DateTime.UtcNow, CandidateId = 1 },
               new Skill { SkillId = 2, Name = "JavaScript", YearsOfExperience = 3, Created = DateTime.UtcNow, CandidateId = 1 },
               new Skill { SkillId = 3, Name = "SQL", YearsOfExperience = 4, Created = DateTime.UtcNow, CandidateId = 2 },
               new Skill { SkillId = 4, Name = "Python", YearsOfExperience = 2, Created = DateTime.UtcNow, CandidateId = 2 }
           );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity is not null))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Candidate candidate)
                    {
                        candidate.Created = DateTime.UtcNow;
                    }
                    else if (entry.Entity is Position position)
                    {
                        position.Created = DateTime.UtcNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Candidate candidate)
                    {
                        candidate.Updated = DateTime.UtcNow;
                    }
                    else if (entry.Entity is Position position)
                    {
                        position.Updated = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

       
    }
}
