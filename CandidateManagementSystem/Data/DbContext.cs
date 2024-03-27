using CandidateManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Data
{
    public class CandidateDBContext: DbContext
    {
        public CandidateDBContext(DbContextOptions<CandidateDBContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<CandidatePosition> CandidatePositions { get; set; }

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
        }
    }
}
