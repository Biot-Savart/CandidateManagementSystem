namespace CandidateManagementSystem.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public int Experience { get; set; } // Represents years of experience
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } // Nullable to allow for non-updated entries
        public DateTime? Archived { get; set; } // Nullable to signify whether it's archived

        // Navigation property for the many-to-many relationship
        public ICollection<CandidatePosition>? CandidatePositions { get; set; }

        public ICollection<Skill>? Skills { get; set; } // Navigation property for skills
    }
}
