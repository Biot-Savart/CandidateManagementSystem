namespace CandidateManagementSystem.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } // Nullable to allow for non-updated entries
        public DateTime? Archived { get; set; } // Nullable to signify whether it's archived

        // Navigation property for the many-to-many relationship
        public ICollection<CandidatePosition>? CandidatePositions { get; set; }
    }
}
